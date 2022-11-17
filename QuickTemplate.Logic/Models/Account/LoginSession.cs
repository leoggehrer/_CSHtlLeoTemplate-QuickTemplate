//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Models.Account
{
    public partial class LoginSession : VersionObject
    {
        public int IdentityId { get; set; }
        public string SessionToken { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? OptionalInfo { get; set; }
        public Role[] Roles { get; set; } = Array.Empty<Role>();

        internal static LoginSession Create(Entities.Account.LoginSession other)
        {
            BeforeCreate(other);
            var result = new LoginSession();

            result.CopyFrom(other);
            result.Roles = other.Roles.Select(r => Role.Create(r)).ToArray();
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate(QuickTemplate.Logic.Entities.Account.LoginSession other);
        static partial void AfterCreate(QuickTemplate.Logic.Models.Account.LoginSession instance, QuickTemplate.Logic.Entities.Account.LoginSession other);
    }
}
#endif
//MdEnd
