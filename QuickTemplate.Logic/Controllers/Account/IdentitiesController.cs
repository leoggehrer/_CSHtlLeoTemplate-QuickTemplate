//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Controllers.Account
{
    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class IdentitiesController : GenericController<Entities.Account.Identity>, Contracts.Account.IIdentitiesAccess<Entities.Account.Identity>
    {
        internal override IEnumerable<string> Includes => new string[] { "Roles" };
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
            return EntitySet.Include(e => e.Roles)
                            .FirstOrDefaultAsync(e => e.State == Modules.Common.State.Active
                                                   && e.AccessFailedCount < 4
                                                   && e.Email.ToLower() == email.ToLower());
        }

        public async Task AddRoleAsync(int id, int roleId)
        {
            await CheckAuthorizationAsync(GetType(), nameof(AddRoleAsync)).ConfigureAwait(false);

            using var roleCtrl = new RolesController(this);
            var role = await roleCtrl.GetByIdAsync(roleId).ConfigureAwait(false);

            if (role != null)
            {
                var entity = await GetByIdAsync(id).ConfigureAwait(false);

                if (entity != null)
                {
                    entity.Roles.Add(role);
                }
            }
        }
        public async Task RemoveRoleAsync(int id, int roleId)
        {
            await CheckAuthorizationAsync(GetType(), nameof(RemoveRoleAsync)).ConfigureAwait(false);

            using var roleCtrl = new RolesController(this);
            var role = await roleCtrl.GetByIdAsync(roleId).ConfigureAwait(false);

            if (role != null)
            {
                var entity = await GetByIdAsync(id).ConfigureAwait(false);

                if (entity != null)
                {
                    entity.Roles.Remove(role);
                }
            }
        }
    }
}
#endif
//MdEnd
