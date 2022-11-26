//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.Logic.Contracts.Account
{
    public partial interface IAccessRulesAccess<T> : IDataAccess<T>
    {
        Task<bool> CanBeCreatedAsync(Type type, IIdentity identity);
        Task<bool> CanBeReadAsync(IIdentifyable item, IIdentity identity);
        Task<bool> CanBeChangedAsync(IIdentifyable item, IIdentity identity);
        Task<bool> CanBeDeletedAsync(IIdentifyable item, IIdentity identity);
    }
}
#endif
//MdEnd
