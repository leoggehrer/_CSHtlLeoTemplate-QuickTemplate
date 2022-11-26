//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Entities.Account
{
    [Table("IdentityXRoles", Schema = "account")]
    [Index(nameof(IdentityId), nameof(RoleId), IsUnique = true)]
    public partial class IdentityXRole : VersionObject
    {
        public IdType IdentityId { get; set; }
        public IdType RoleId { get; set; }

        // Navigation properties
        public Identity? Identity { get; set; }
        public Role? Role { get; set; }
    }
}
#endif
//MdEnd
