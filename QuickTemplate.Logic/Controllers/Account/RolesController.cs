//@CodeCopy
//MdStart
#if ACCOUNT_ON

namespace QuickTemplate.Logic.Controllers.Account
{
    [Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    internal sealed partial class RolesController : GenericController<Entities.Account.Role>, Contracts.Account.IRolesAccess<Entities.Account.Role>
    {
        public RolesController()
        {
        }

        public RolesController(ControllerObject other) : base(other)
        {
        }

        protected override void BeforeActionExecute(ActionType actionType, Entities.Account.Role entity)
        {
            if (actionType == ActionType.Insert)
            {
                entity.Guid = Guid.NewGuid();
            }
            else if (actionType == ActionType.Update)
            {
                using var ctrl = new RolesController();
                var dbEntity = ctrl.EntitySet.Find(entity.Id);

                if (dbEntity != null)
                {
                    entity.Guid = dbEntity.Guid;
                }
            }
            base.BeforeActionExecute(actionType, entity);
        }
    }
}
#endif
//MdEnd
