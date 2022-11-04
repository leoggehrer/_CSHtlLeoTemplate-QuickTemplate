//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Controllers.Account
{
    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class IdentitiesController : GenericController<Entities.Account.Identity>, Contracts.Account.IIdentitiesAccess<Entities.Account.Identity>
    {
        public IdentitiesController()
        {
        }

        public IdentitiesController(ControllerObject other) : base(other)
        {
        }

        protected override void BeforeActionExecute(ActionType actionType, Entities.Account.Identity entity)
        {
            if (actionType == ActionType.Insert)
            {
                entity.Guid = Guid.NewGuid().ToString();
            }
            else if (actionType == ActionType.Update)
            {
                using var ctrl = new IdentitiesController();
                var dbEntity = ctrl.EntitySet.Find(entity.Id);

                if (dbEntity != null)
                {
                    entity.Guid = dbEntity.Guid;
                }
            }
            base.BeforeActionExecute(actionType, entity);
        }
        internal Task<Entities.Account.Identity?> GetValidIdentityByEmailAsync(string email)
        {
            return EntitySet.Include(e => e.IdentityXRoles)
                            .ThenInclude(e => e.Role)
                            .FirstOrDefaultAsync(e => e.State == Modules.Common.State.Active
                                                   && e.AccessFailedCount < 4
                                                   && e.Email.ToLower() == email.ToLower());
        }

        public async Task AddRoleAsync(int identityId, int roleId)
        {
            await CheckAuthorizationAsync(GetType(), nameof(AddRoleAsync)).ConfigureAwait(false);

            using var roleCtrl = new RolesController(this);
            var role = await roleCtrl.ExecuteGetByIdAsync(roleId).ConfigureAwait(false);

            if (role != null)
            {
                var identity = await ExecuteGetByIdAsync(identityId).ConfigureAwait(false);

                if (identity != null)
                {
                    using var identityXRolesCtrl = new IdentityXRolesController(this);
                    var identityXRole = new Entities.Account.IdentityXRole
                    {
                        Role = role,
                        Identity = identity,
                    };
                    await identityXRolesCtrl.InsertAsync(identityXRole).ConfigureAwait(false);
                }
            }
        }
        public async Task RemoveRoleAsync(int identityId, int roleId)
        {
            await CheckAuthorizationAsync(GetType(), nameof(RemoveRoleAsync)).ConfigureAwait(false);

            using var identityXRolesCtrl = new IdentityXRolesController(this);
            var identityXRoles = await identityXRolesCtrl.ExecuteQueryAsync(e => e.IdentityId == identityId && e.RoleId == roleId)
                                                         .ConfigureAwait(false);

            if (identityXRoles.Length == 1)
            {
                await identityXRolesCtrl.DeleteAsync(identityXRoles[0].Id).ConfigureAwait(false);
            }
        }
    }
}
#endif
//MdEnd
