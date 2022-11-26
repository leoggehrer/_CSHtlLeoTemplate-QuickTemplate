//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.Logic.Controllers.Account
{
    using QuickTemplate.Logic.Contracts;
    using QuickTemplate.Logic.Entities.Account;
    using QuickTemplate.Logic.Modules.Exceptions;

    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class AccessRulesController : GenericController<AccessRule>, Contracts.Account.IAccessRulesAccess<AccessRule>
    {
        private List<AccessRule> AccessRules = new List<AccessRule>();

        public AccessRulesController()
        {
        }

        public AccessRulesController(ControllerObject other) : base(other)
        {
        }

        protected override void ValidateEntity(ActionType actionType, AccessRule entity)
        {
            // Check rule type
            if (entity.Type == Modules.Account.RuleType.EntityType && string.IsNullOrEmpty(entity.EntityValue) == false)
            {
                throw new LogicException(ErrorType.InvalidAccessRuleEntityValue);
            }
            else if (entity.Type == Modules.Account.RuleType.EntityBy && string.IsNullOrEmpty(entity.EntityValue))
            {
                throw new LogicException(ErrorType.InvalidAccessRuleEntityValue);
            }
            else if (entity.Type == Modules.Account.RuleType.Entities && string.IsNullOrEmpty(entity.EntityValue) == false)
            {
                throw new LogicException(ErrorType.InvalidAccessRuleEntityValue);
            }
            // Check access type
            else if (entity.AccessType == Modules.Account.AccessType.All && string.IsNullOrEmpty(entity.AccessValue) == false)
            {
                throw new LogicException(ErrorType.InvalidAccessRuleAccessValue);
            }
            else if (entity.AccessType == Modules.Account.AccessType.Identity && string.IsNullOrEmpty(entity.AccessValue))
            {
                throw new LogicException(ErrorType.InvalidAccessRuleAccessValue);
            }
            else if (entity.AccessType == Modules.Account.AccessType.Role && string.IsNullOrEmpty(entity.AccessValue))
            {
                throw new LogicException(ErrorType.InvalidAccessRuleAccessValue);
            }
            base.ValidateEntity(actionType, entity);
        }
        protected override void BeforeActionExecute(ActionType actionType, AccessRule entity)
        {
            if (actionType == ActionType.Insert || actionType == ActionType.Update)
            {
                var dbAccessRule = EntitySet.FirstOrDefault(e => e.Id != entity.Id
                                                              && e.EntityType == entity.EntityType
                                                              && e.RelationshipEntityType == entity.RelationshipEntityType
                                                              && e.AccessType == entity.AccessType
                                                              && e.AccessValue == entity.AccessValue);
                if (dbAccessRule != null)
                {
                    throw new LogicException(ErrorType.InvalidAccessRuleAlreadyExits);
                }
            }
            base.BeforeActionExecute(actionType, entity);
        }
        private async Task<IEnumerable<AccessRule>> GetAccessRulesAsync(string entityType)
        {
            var result = AccessRules.Where(ar => ar.EntityType == entityType).ToList();

            if (result.Any() == false)
            {
                result.AddRange(await EntitySet.Where(ar => ar.EntityType == entityType).ToArrayAsync().ConfigureAwait(false));
            }
            return result;
        }

        public Task<bool> CanBeCreatedAsync(Type type, Contracts.Account.IIdentity identity)
        {
            return GetCreateAccessAsync(type, identity);
        }
        public Task<bool> CanBeReadAsync(IIdentifyable item, Contracts.Account.IIdentity identity)
        {
            return GetReadAccessAsync(item, identity);
        }
        public Task<bool> CanBeChangedAsync(IIdentifyable item, Contracts.Account.IIdentity identity)
        {
            return GetUpdateAccessAsync(item, identity);
        }
        public Task<bool> CanBeDeletedAsync(IIdentifyable item, Contracts.Account.IIdentity identity)
        {
            return GetDeleteAccessAsync(item, identity);
        }

        private async Task<bool> GetCreateAccessAsync(Type type, Contracts.Account.IIdentity identity)
        {
            Func<AccessRule, bool> getOperation = ar => ar.Creatable;
            var accessRules = await GetAccessRulesAsync(type.Name).ConfigureAwait(false);

            return GetEntityTypeAccess(accessRules, identity, getOperation);
        }
        private async Task<bool> GetReadAccessAsync(IIdentifyable item, Contracts.Account.IIdentity identity)
        {
            Func<AccessRule, bool> getOperation = ar => ar.Readable;
            var accessRules = await GetAccessRulesAsync(item.GetType().Name).ConfigureAwait(false);

            return GetEntityTypeAccess(accessRules, identity, getOperation) 
                && GetEntitiesAccess(accessRules, identity, getOperation) 
                && GetEntityByAccess(accessRules, item, identity, getOperation);
        }
        private async Task<bool> GetUpdateAccessAsync(IIdentifyable item, Contracts.Account.IIdentity identity)
        {
            Func<AccessRule, bool> getOperation = ar => ar.Updatable;
            var accessRules = await GetAccessRulesAsync(item.GetType().Name).ConfigureAwait(false);

            return GetEntityTypeAccess(accessRules, identity, getOperation)
                && GetEntitiesAccess(accessRules, identity, getOperation)
                && GetEntityByAccess(accessRules, item, identity, getOperation);
        }
        private async Task<bool> GetDeleteAccessAsync(IIdentifyable item, Contracts.Account.IIdentity identity)
        {
            Func<AccessRule, bool> getOperation = ar => ar.Deletable;
            var accessRules = await GetAccessRulesAsync(item.GetType().Name).ConfigureAwait(false);

            return GetEntityTypeAccess(accessRules, identity, getOperation)
                && GetEntitiesAccess(accessRules, identity, getOperation)
                && GetEntityByAccess(accessRules, item, identity, getOperation);
        }

        private bool GetEntityTypeAccess(IEnumerable<AccessRule> accessRules, Contracts.Account.IIdentity identity, Func<AccessRule, bool> getOperation)
        {
            var result = false;
            var typeRules = accessRules.Where(ar => ar.Type == Modules.Account.RuleType.EntityType);

            if (typeRules.Any())
            {
                var accessRule = typeRules.FirstOrDefault(r => r.AccessType == Modules.Account.AccessType.All);

                if (accessRule != null)
                {
                    result = getOperation(accessRule);
                }
                accessRule = typeRules.FirstOrDefault(r => r.AccessType == Modules.Account.AccessType.Identity && r.AccessValue == identity.Guid.ToString());
                if (accessRule != null)
                {
                    result = getOperation(accessRule);
                }
                accessRule = typeRules.FirstOrDefault(r => r.AccessType == Modules.Account.AccessType.Role && identity.HasRole(Guid.Parse(r.AccessValue!)));
                if (accessRule != null)
                {
                    result = getOperation(accessRule);
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
        private bool GetEntitiesAccess(IEnumerable<AccessRule> accessRules, Contracts.Account.IIdentity identity, Func<AccessRule, bool> getOperation)
        {
            var result = false;
            var typeRules = accessRules.Where(ar => ar.Type == Modules.Account.RuleType.Entities);

            if (typeRules.Any())
            {
                var accessRule = typeRules.FirstOrDefault(r => r.AccessType == Modules.Account.AccessType.All);

                if (accessRule != null)
                {
                    result = getOperation(accessRule);
                }
                accessRule = typeRules.FirstOrDefault(r => r.AccessType == Modules.Account.AccessType.Identity && r.AccessValue == identity.Guid.ToString());
                if (accessRule != null)
                {
                    result = getOperation(accessRule);
                }
                accessRule = typeRules.FirstOrDefault(r => r.AccessType == Modules.Account.AccessType.Role && identity.HasRole(Guid.Parse(r.AccessValue!)));
                if (accessRule != null)
                {
                    result = getOperation(accessRule);
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
        private bool GetEntityByAccess(IEnumerable<AccessRule> accessRules, IIdentifyable item, Contracts.Account.IIdentity identity, Func<AccessRule, bool> getOperation)
        {
            var result = false;
            var typeRules = accessRules.Where(ar => ar.Type == Modules.Account.RuleType.EntityBy && ar.EntityValue == item.Id.ToString());

            if (typeRules.Any())
            {
                var accessRule = typeRules.FirstOrDefault(r => r.AccessType == Modules.Account.AccessType.All);

                if (accessRule != null)
                {
                    result = getOperation(accessRule);
                }
                accessRule = typeRules.FirstOrDefault(r => r.AccessType == Modules.Account.AccessType.Identity && r.AccessValue == identity.Guid.ToString());
                if (accessRule != null)
                {
                    result = getOperation(accessRule);
                }
                accessRule = typeRules.FirstOrDefault(r => r.AccessType == Modules.Account.AccessType.Role && identity.HasRole(Guid.Parse(r.AccessValue!)));
                if (accessRule != null)
                {
                    result = getOperation(accessRule);
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
#endif
//MdEnd
