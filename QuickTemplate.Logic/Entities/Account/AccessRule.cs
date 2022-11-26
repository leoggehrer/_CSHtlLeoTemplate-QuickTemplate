//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.Logic.Entities.Account
{
    using QuickTemplate.Logic.Modules.Account;

    [Table("AccessRules", Schema = "account")]
    [Index(nameof(EntityType), IsUnique = false)]
    internal partial class AccessRule : VersionObject
    {
        public RuleType Type { get; set; }
        [MaxLength(128)]
        public string EntityType { get; set; } = string.Empty;
        [MaxLength(128)]
        public string? RelationshipEntityType { get; set; } = string.Empty;
        [MaxLength(128)]
        public string? PropertyName { get; set; }
        [MaxLength(36)]
        public string? EntityValue { get; set; }
        public AccessType AccessType { get; set; }
        [MaxLength(36)]
        public string? AccessValue { get; set; }
        public bool Creatable { get; set; }
        public bool Readable { get; set; }
        public bool Updatable { get; set; }
        public bool Deletable { get; set; }
        public bool Viewable { get; set; } = true;
    }
}
#endif
//MdEnd
