//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.AspMvc.Models.Account
{
    using QuickTemplate.Logic.Modules.Common;
    public class Identity : VersionModel
    {
        public IdentityRole[] AccessRoleList { get; set; } = Array.Empty<IdentityRole>();

        public IdentityRole[] AddAccessRoleList
        {
            get
            {
                IdentityRole[] result;

                if (AccessRoleList != null)
                {
                    result = AccessRoleList.Where(e => AccessRoles.Any(m => m.Id == e.Id) == false).ToArray();
                }
                else
                {
                    result = Array.Empty<IdentityRole>();
                }
                return result;
            }
        }
        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TimeOutInMinutes { get; set; } = 30;
        public int AccessFailedCount { get; set; }
        public State State { get; set; } = State.Active;

        public List<IdentityRole> AccessRoles { get; set; } = new();
        public static Identity Create(Logic.Models.Account.Identity source)
        {
            var result = new Identity();

            result.CopyFrom(source);
            return result;
        }
    }
}
#endif
//MdEnd
