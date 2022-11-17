//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Entities.Account
{
    using QuickTemplate.Logic.Modules.Common;

    [Table("AccessRules", Schema = "account")]
    [Index(nameof(EntityTypeName), IsUnique = false)]
    internal partial class AccessRule : VersionObject
    {
        public int? AccessRuleId_Association { get; set; }
        public RuleType Type { get; set; }
        [MaxLength(128)]
        public string EntityTypeName { get; set; } = string.Empty;
        [MaxLength(128)]
        public string? PropertyName { get; set; }
        [MaxLength(36)]
        public string? EntityValue { get; set; }
        public AccessUnit AccessUnit { get; set; }
        [MaxLength(36)]
        public string? AccessUnitValue { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Display { get; set; } = true;
    }
}
#endif
//MdEnd
