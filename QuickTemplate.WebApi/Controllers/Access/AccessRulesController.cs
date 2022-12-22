//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.WebApi.Controllers.Access
{
    ///
    /// Implemented from template developer
    ///
    public sealed partial class AccessRulesController : GenericController<Logic.Models.Access.AccessRule, WebApi.Models.Access.AccessRuleEdit, WebApi.Models.Access.AccessRule>
    {
        static AccessRulesController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        ///
        /// Implemented from template developer
        ///
        public AccessRulesController(Logic.Contracts.Access.IAccessRulesAccess<Logic.Models.Access.AccessRule> other)
        : base(other)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Implemented from template developer
        ///
        protected override QuickTemplate.WebApi.Models.Access.AccessRule ToOutModel(QuickTemplate.Logic.Models.Access.AccessRule accessModel)
        {
            var handled = false;
            var result = default(QuickTemplate.WebApi.Models.Access.AccessRule);
            BeforeToOutModel(accessModel, ref result, ref handled);
            if (handled == false || result == null)
            {
                result = QuickTemplate.WebApi.Models.Access.AccessRule.Create(accessModel);
            }
            AfterToOutModel(result);
            return result;
        }
        partial void BeforeToOutModel(QuickTemplate.Logic.Models.Access.AccessRule accessModel, ref QuickTemplate.WebApi.Models.Access.AccessRule? outModel, ref bool handled);
        partial void AfterToOutModel(QuickTemplate.WebApi.Models.Access.AccessRule outModel);
    }
}
#endif
//MdEnd
