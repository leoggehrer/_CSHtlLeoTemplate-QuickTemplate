//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Facades.Account
{
    using TOutModel = Models.Account.Identity;
    partial class IdentitiesFacade
    {
        new private Contracts.Account.IIdentitiesAccess<TOutModel> Controller => (ControllerObject as Contracts.Account.IIdentitiesAccess<TOutModel>)!;
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
