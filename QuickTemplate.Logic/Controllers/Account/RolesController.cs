//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Controllers.Account
{
    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class RolesController : GenericController<Entities.Account.Role>, Contracts.Account.IRolesAccess<Entities.Account.Role>
    {
        public RolesController()
        {
        }

        public RolesController(ControllerObject other) : base(other)
        {
        }
    }
}
#endif
//MdEnd
