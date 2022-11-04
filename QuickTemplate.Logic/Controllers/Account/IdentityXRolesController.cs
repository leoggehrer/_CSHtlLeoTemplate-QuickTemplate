//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Controllers.Account
{
    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class IdentityXRolesController : GenericController<Entities.Account.IdentityXRole>, Contracts.Account.IRolesAccess<Entities.Account.IdentityXRole>
    {
        public IdentityXRolesController()
        {
        }

        public IdentityXRolesController(ControllerObject other) : base(other)
        {
        }
    }
}
#endif
//MdEnd
