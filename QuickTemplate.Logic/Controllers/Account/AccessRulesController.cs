//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Controllers.Account
{
    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class AccessRulesController : GenericController<Entities.Account.AccessRule>, Contracts.Account.IAccessRulesAccess<Entities.Account.AccessRule>
    {
        internal override IEnumerable<string> Includes => new string[] { "Identity" };
        public AccessRulesController()
        {
        }

        public AccessRulesController(ControllerObject other) : base(other)
        {
        }
    }
}
#endif
//MdEnd
