//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Controllers.Account
{
    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class UsersController : GenericController<Entities.Account.User>, Contracts.Account.IUsersAccess<Entities.Account.User>
    {
        internal override IEnumerable<string> Includes => new string[] { "Identity" };
        public UsersController()
        {
        }

        public UsersController(ControllerObject other) : base(other)
        {
        }
    }
}
#endif
//MdEnd
