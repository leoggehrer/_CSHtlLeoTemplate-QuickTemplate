﻿//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Entities.Account
{
#if SQLITE_ON
    [Table("Roles")]
#else
    [Table("Roles", Schema = "account")]
#endif
    [Index(nameof(Designation), IsUnique = true)]
    public partial class Role : VersionObject, Contracts.Account.IRole
    {
        public Guid Guid { get; internal set; }
        [MaxLength(64)]
        public string Designation { get; set; } = string.Empty;
        [MaxLength(256)]
        public string? Description { get; set; }
    }
}
#endif
//MdEnd
