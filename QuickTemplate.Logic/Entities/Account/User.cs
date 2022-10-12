//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Entities.Account
{
    [Table("Users", Schema = "account")]
    internal partial class User : VersionEntity
    {
        public int IdentityId { get; set; }
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(64)]
        public string LastName { get; set; } = string.Empty;

        // Navigation properties
        public Identity? Identity { get; set; }
    }
}
#endif
//MdEnd
