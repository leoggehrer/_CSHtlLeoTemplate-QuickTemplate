//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
using QuickTemplate.Logic.Entities;

namespace QuickTemplate.Logic.Controllers
{
    public abstract partial class AccessGenericController<TEntity> : GenericController<TEntity>
        where TEntity : Entities.EntityObject, new()
    {
        /// <summary>
        /// Creates an instance.
        /// </summary>
        public AccessGenericController()
            : base()
        {
        }
        /// <summary>
        /// Creates an instance.
        /// </summary>
        /// <param name="other">A reference to an other controller</param>
        public AccessGenericController(ControllerObject other)
            : base(other)
        {
        }

        #region Insert
        public override async Task<TEntity> InsertAsync(TEntity entity)
        {
            await CheckAuthorizationAsync(GetType(), nameof(InsertAsync)).ConfigureAwait(false);
            await CheckCanBeCreatedAsync(typeof(TEntity)).ConfigureAwait(false);

            var result = await ExecuteInsertAsync(entity).ConfigureAwait(false);

            return BeforeReturn(result);
        }
        #endregion Insert

        #region Update
        public override async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await CheckAuthorizationAsync(GetType(), nameof(InsertAsync)).ConfigureAwait(false);
            await CheckCanBeChangedAsync(entity).ConfigureAwait(false);

            var result = ExecuteUpdate(entity);

            return BeforeReturn(result);
        }
        #endregion Update

        #region Delete
        public override async Task DeleteAsync(IdType id)
        {
            await CheckAuthorizationAsync(GetType(), nameof(DeleteAsync), id.ToString()).ConfigureAwait(false);

            TEntity? entity = await EntitySet.FindAsync(id).ConfigureAwait(false);

            if (entity != null)
            {
                await CheckCanBeDeletedAsync(entity).ConfigureAwait(false);
                
                ExecuteDelete(entity);
            }
        }
        #endregion Delete
    }
}
#endif
//MdEnd
