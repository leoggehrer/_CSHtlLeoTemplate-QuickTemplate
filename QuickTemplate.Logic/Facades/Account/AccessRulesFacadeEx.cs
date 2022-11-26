﻿//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.Logic.Facades.Account
{
    partial class AccessRulesFacade
    {
        public Task<bool> CanBeCreatedAsync(Type type, Contracts.Account.IIdentity identity)
        {
            return Controller.CanBeCreatedAsync(type, identity);
        }
        public Task<bool> CanBeReadAsync(Contracts.IIdentifyable model, Contracts.Account.IIdentity identity)
        {
            return Controller.CanBeReadAsync(model, identity);
        }
        public Task<bool> CanBeChangedAsync(Contracts.IIdentifyable model, Contracts.Account.IIdentity identity)
        {
            return Controller.CanBeChangedAsync(model, identity);
        }
        public Task<bool> CanBeDeletedAsync(Contracts.IIdentifyable model, Contracts.Account.IIdentity identity)
        {
            return Controller.CanBeDeletedAsync(model, identity);
        }
    }
}
#endif
//MdEnd
