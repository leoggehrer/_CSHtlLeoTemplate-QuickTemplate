//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.AspMvc.Models.Account
{
    using QuickTemplate.Logic.Modules.Account;
    public partial class AccessRule : VersionModel
    {
        public RuleType Type { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public string? RelationshipEntityType { get; set; }
        public string? PropertyName { get; set; }
        public string? EntityValue { get; set; }
        public AccessType AccessType { get; set; }
        public string? AccessValue { get; set; }
        public bool Creatable { get; set; }
        public bool Readable { get; set; }
        public bool Updatable { get; set; }
        public bool Deletable { get; set; }
        public bool Viewable { get; set; }

        public static AccessRule Create(Logic.Models.Account.AccessRule source)
        {
            var result = new AccessRule();

            result.CopyFrom(source);
            return result;
        }
    }
}
#endif
//MdEnd
