//@CodeCopy
//MdStart
#if ACCOUNT_ON

namespace QuickTemplate.Logic.Facades.Account
{
    partial class IdentitiesFacade
    {
        public Task AddRoleAsync(int id, int roleId)
        {
            return Controller.AddRoleAsync(id, roleId);
        }
        public Task RemoveRoleAsync(int id, int roleId)
        {
            return Controller.RemoveRoleAsync(id, roleId);
        }
    }
}
#endif
//MdEnd
