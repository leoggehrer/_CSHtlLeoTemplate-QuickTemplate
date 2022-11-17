//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Entities.Account
{
    [Table("SecureIdentities", Schema = "account")]
    [Index(nameof(Email), IsUnique = true)]
    internal partial class SecureIdentity : Identity
    {
        [Required]
        [MaxLength(512)]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        [Required]
        [MaxLength(512)]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        #region Transient properties
        [NotMapped]
        public string Password { get; set; } = string.Empty;
        #endregion Transient properties

        // Navigation properties
        public List<LoginSession> LoginSessions { get; set; } = new();
    }
}
#endif
//MdEnd
