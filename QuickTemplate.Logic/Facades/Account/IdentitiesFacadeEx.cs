//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Facades.Account
{
    partial class IdentitiesFacade
    {
        public Task AddRoleAsync(IdType id, IdType roleId)
        {
            return Controller.AddRoleAsync(id, roleId);
        }
        public Task RemoveRoleAsync(IdType id, IdType roleId)
        {
            return Controller.RemoveRoleAsync(id, roleId);
        }
    }
}
#endif
//MdEnd
