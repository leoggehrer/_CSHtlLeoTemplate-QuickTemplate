//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Entities.Account
{
    [Table("Roles", Schema = "account")]
    [Index(nameof(Designation), IsUnique = true)]
    public partial class Role : VersionObject
    {
        public Guid Guid { get; internal set; } = Guid.NewGuid();
        [MaxLength(64)]
        public string Designation { get; set; } = string.Empty;
        [MaxLength(256)]
        public string? Description { get; set; }
    }
}
#endif
//MdEnd
