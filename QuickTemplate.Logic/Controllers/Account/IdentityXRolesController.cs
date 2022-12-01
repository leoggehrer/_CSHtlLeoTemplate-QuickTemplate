//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Controllers.Account
{
    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class IdentityXRolesController : GenericController<Entities.Account.IdentityXRole>, Contracts.Account.IRolesAccess<Entities.Account.IdentityXRole>
    {
        //internal override IEnumerable<string> Includes => new string[] { nameof(Entities.Account.IdentityXRole.Role), nameof(Entities.Account.Identity) };
        public IdentityXRolesController()
        {
        }

        public IdentityXRolesController(ControllerObject other) : base(other)
        {
        }

        public Task<Entities.Account.IdentityXRole[]> QueryByIdentityAsync(IdType identityId)
        {
            var query = EntitySet.AsQueryable();

            query = query.Include(e => e.Role);
            query = query.Where(e => e.IdentityId == identityId);

            return query.AsNoTracking().ToArrayAsync();
        }
    }
}
#endif
//MdEnd
