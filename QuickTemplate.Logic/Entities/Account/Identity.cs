﻿//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Entities.Account
{
    using QuickTemplate.Logic.Modules.Common;

    [Table("Identities", Schema = "account")]
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    internal partial class Identity : VersionEntity
    {
        [MaxLength(36)]
        public string Guid { get; internal set; } = string.Empty;
        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(128)]
        public string Email { get; set; } = string.Empty;
        public int TimeOutInMinutes { get; set; } = 30;
        public bool EnableJwtAuth { get; set; }
        public int AccessFailedCount { get; set; }
        public State State { get; set; } = State.Active;

        [Required]
        [MaxLength(512)]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        [Required]
        [MaxLength(512)]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        #region transient properties
        [NotMapped]
        public string Password { get; set; } = string.Empty;
        #endregion transient properties

        // Navigation properties
        public List<IdentityXRole> IdentityXRoles { get; set; } = new();
        public List<LoginSession> LoginSessions { get; set; } = new();
    }
}
#endif
//MdEnd
