//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Facades.Account
{
    using QuickTemplate.Logic.Entities.Account;
    partial class IdentitiesFacade
    {
        partial void BeforeToEntity(Models.Account.Identity model, ref SecureIdentity? entity, ref bool handled)
        {
            entity = Task.Run(async () => await Controller.GetByIdAsync(model.Id)).Result;

            if (entity == null)
                throw new Modules.Exceptions.LogicException(Modules.Exceptions.ErrorType.InvalidId);

            entity.CopyFrom(model);
            handled = true;
        }
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
