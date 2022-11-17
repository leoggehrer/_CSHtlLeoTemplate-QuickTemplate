//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Entities.Account
{
    using QuickTemplate.Logic.Modules.Common;

    [Table("Identities", Schema = "account")]
    [Index(nameof(Email), IsUnique = true)]
    public abstract partial class Identity : VersionObject
    {
        public Guid Guid { get; internal set; } = Guid.NewGuid();
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(128)]
        public string Email { get; set; } = string.Empty;
        public int TimeOutInMinutes { get; set; } = 30;
        public bool EnableJwtAuth { get; set; }
        public int AccessFailedCount { get; set; }
        public State State { get; set; } = State.Active;

        // Navigation properties
        public List<IdentityXRole> IdentityXRoles { get; set; } = new();
    }
}
#endif
//MdEnd
