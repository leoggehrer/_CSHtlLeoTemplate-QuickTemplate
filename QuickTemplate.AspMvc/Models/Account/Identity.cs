//@CodeCopy
//MdStart
#if ACCOUNT_ON
using QuickTemplate.Logic.Modules.Common;

namespace QuickTemplate.AspMvc.Models.Account
{
    public class Identity : VersionModel
    {
        public AccessRole[]? AccessRoleList { get; set; }

        public AccessRole[] AddAccessRoleList
        {
            get
            {
                AccessRole[] result;

                if (AccessRoleList != null)
                {
                    result = AccessRoleList.Where(e => AccessRoles.Any(m => m.Id == e.Id) == false).ToArray();
                }
                else
                {
                    result = Array.Empty<AccessRole>();
                }
                return result;
            }
        }

        public string Guid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TimeOutInMinutes { get; set; } = 30;
        public int AccessFailedCount { get; set; }
        public State State { get; set; } = State.Active;

        public List<AccessRole> AccessRoles { get; set; } = new();
        public static Identity Create(Logic.Models.Account.Identity source)
        {
            var result = new Identity();

            result.Id = source.Id;
            result.RowVersion = source.RowVersion;
            result.Guid = source.Guid;
            result.Name = source.Name;
            result.Email = source.Email;
            result.TimeOutInMinutes = source.TimeOutInMinutes;
            result.AccessFailedCount = source.AccessFailedCount;
            result.State = source.State;

            result.AccessRoles = source.Roles.Select(r => Models.Account.AccessRole.Create(r)).ToList();
            return result;
        }
    }
}
#endif
//MdEnd
