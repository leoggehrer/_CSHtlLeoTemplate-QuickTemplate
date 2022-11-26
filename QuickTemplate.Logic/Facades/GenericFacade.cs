//@CodeCopy
//MdStart

using QuickTemplate.Logic.Contracts;
using QuickTemplate.Logic.Controllers;

namespace QuickTemplate.Logic.Facades
{
    /// <summary>
    /// Generic facade for mapping entity types to model types.
    /// </summary>
    /// <typeparam name="TModel">The model type as public type</typeparam>
    /// <typeparam name="TEntity">The entity type for the internal controller</typeparam>
    /// <inheritdoc cref="IDataAccess"/>
    internal abstract partial class GenericFacade<TModel, TEntity> : FacadeObject, IDataAccess<TModel>
        where TModel : Models.ModelObject, new()
        where TEntity : Entities.EntityObject, new()
    {
        protected GenericController<TEntity> Controller { get; init; }
        protected GenericFacade(GenericController<TEntity> controller)
            : base(controller)
        {
            Controller = controller;
        }
        /// <summary>
        /// Converts the entity type to the facade type.
        /// </summary>
        /// <param name="entity">Entity type</param>
        /// <returns>The facade type</returns>
        internal virtual TModel ToModel(TEntity entity)
        {
            var result = new TModel
            {
                Source = entity
            };
            return result;
        }
        /// <summary>
        /// Converts the model type to the entity type.
        /// </summary>
        /// <param name="model">Model type</param>
        /// <returns>The entity type</returns>
        internal virtual TEntity ToEntity(TModel model)
        {
            var result = model.Source as TEntity;

            return result!;
        }

#if ACCOUNT_ON
        #region SessionToken
        /// <summary>
        /// Sets the authorization token.
        /// </summary>
        public string SessionToken
        {
            set
            {
                Controller.SessionToken = value;
            }
        }
        #endregion SessionToken
#endif

        #region Create
        /// <summary>
        /// Creates a new element of type TModel.
        /// </summary>
        /// <returns>The new element.</returns>
        public TModel Create()
        {
            var entity = Controller.Create();

            return ToModel(entity);
        }
        #endregion Create

        #region MaxPageSize and Count
        /// <summary>
        /// Gets the maximum page size.
        /// </summary>
        public virtual int MaxPageSize => Controller.MaxPageSize;
        /// <summary>
        /// Gets the number of quantity in the collection.
        /// </summary>
        /// <returns>Number of elements in the collection.</returns>
        public virtual Task<int> CountAsync()
        {
            return Controller.CountAsync();
        }
        /// <summary>
        /// Returns the number of quantity in the collection based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <returns>Number of entities in the collection.</returns>
        public virtual Task<int> CountAsync(string predicate)
        {
            return Controller.CountAsync(predicate);
        }
        /// <summary>
        /// Returns the number of quantity in the collection based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Number of entities in the collection.</returns>
        public virtual Task<int> CountAsync(string predicate, params string[] includeItems)
        {
            return Controller.CountAsync(predicate, includeItems);
        }
        #endregion MaxPageSize and Count

        #region Queries
#if GUID_ON
        /// <summary>
        /// Returns the element of type T with the identification of id.
        /// </summary>
        /// <param name="id">The identification.</param>
        /// <returns>The element of the type T with the corresponding identification.</returns>
        public async Task<TModel?> GetByGuidAsync(Guid id)
        {
            var handled = false;
            var result = default(TModel);

            BeforeGetByGuid(ref result, id, ref handled);
            if (handled == false || result == null)
            {
                var entity = await Controller.GetByGuidAsync(id).ConfigureAwait(false);

                if (entity != null)
                    result = ToModel(entity);
            }
            AfterGetByGuid(result);
            return result;
        }
        partial void BeforeGetByGuid(ref TModel? model, Guid id, ref bool handled);
        partial void AfterGetByGuid(TModel? model);
#endif
        /// <summary>
        /// Returns the element of type T with the identification of id.
        /// </summary>
        /// <param name="id">The identification.</param>
        /// <returns>The element of the type T with the corresponding identification.</returns>
        public virtual async Task<TModel?> GetByIdAsync(IdType id)
        {
            var entity = await Controller.GetByIdAsync(id).ConfigureAwait(false);

            return entity != null ? ToModel(entity) : null;
        }
        /// <summary>
        /// Returns the element of type T with the identification of id.
        /// </summary>
        /// <param name="id">The identification.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>The element of the type T with the corresponding identification (with includes).</returns>
        public virtual async Task<TModel?> GetByIdAsync(IdType id, params string[] includeItems)
        {
            var entity = await Controller.GetByIdAsync(id, includeItems).ConfigureAwait(false);

            return entity != null ? ToModel(entity) : null;
        }

        /// <summary>
        /// Returns all objects of the elements in the collection.
        /// </summary>
        /// <returns>All objects of the element collection.</returns>
        public virtual async Task<TModel[]> GetAllAsync()
        {
            var entities = await Controller.GetAllAsync().ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Returns all objects of the elements in the collection.
        /// </summary>
        /// <param name="includeItems">The include items</param>
        /// <returns>All objects of the element collection.</returns>
        public virtual async Task<TModel[]> GetAllAsync(params string[] includeItems)
        {
            var entities = await Controller.GetAllAsync(includeItems).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Returns all elements in the collection.
        /// </summary>
        /// <param name="orderBy">Sorts the elements of a sequence according to a sort clause.</param>
        /// <returns>All interfaces of the element collection.</returns>
        public virtual async Task<TModel[]> GetAllAsync(string orderBy)
        {
            var entities = await Controller.GetAllAsync(orderBy).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Returns all interfaces of the elements in the collection.
        /// </summary>
        /// <param name="includeItems">The include items</param>
        /// <returns>All interfaces of the element collection.</returns>
        public virtual async Task<TModel[]> GetAllAsync(string orderBy, params string[] includeItems)
        {
            var entities = await Controller.GetAllAsync(orderBy, includeItems).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }

        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public virtual async Task<TModel[]> GetPageListAsync(int pageIndex, int pageSize)
        {
            var entities = await Controller.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public virtual async Task<TModel[]> GetPageListAsync(int pageIndex, int pageSize, params string[] includeItems)
        {
            var entities = await Controller.GetPageListAsync(pageIndex, pageSize, includeItems).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public virtual async Task<TModel[]> GetPageListAsync(string orderBy, int pageIndex, int pageSize)
        {
            var entities = await Controller.GetPageListAsync(orderBy, pageIndex, pageSize).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public virtual async Task<TModel[]> GetPageListAsync(string orderBy, int pageIndex, int pageSize, params string[] includeItems)
        {
            var entities = await Controller.GetPageListAsync(orderBy, pageIndex, pageSize, includeItems).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }

        /// <summary>
        /// Filters a sequence of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <returns>The filter result.</returns>
        public virtual async Task<TModel[]> QueryAsync(string predicate)
        {
            var entities = await Controller.QueryAsync(predicate).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public virtual async Task<TModel[]> QueryAsync(string predicate, params string[] includeItems)
        {
            var entities = await Controller.QueryAsync(predicate, includeItems).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Filters a sequence of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <returns>The filter result.</returns>
        public virtual async Task<TModel[]> QueryAsync(string predicate, string orderBy)
        {
            var entities = await Controller.QueryAsync(predicate, orderBy).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Filters a sequence of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>The filter result.</returns>
        public virtual async Task<TModel[]> QueryAsync(string predicate, string orderBy, params string[] includeItems)
        {
            var entities = await Controller.QueryAsync(predicate, orderBy, includeItems).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }

        /// <summary>
        /// Filters a subset of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public virtual async Task<TModel[]> QueryAsync(string predicate, int pageIndex, int pageSize)
        {
            var entities = await Controller.QueryAsync(predicate, pageIndex, pageSize).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Filters a subset of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public virtual async Task<TModel[]> QueryAsync(string predicate, int pageIndex, int pageSize, params string[] includeItems)
        {
            var entities = await Controller.QueryAsync(predicate, pageIndex, pageSize, includeItems).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Filters a subset of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public virtual async Task<TModel[]> QueryAsync(string predicate, string orderBy, int pageIndex, int pageSize)
        {
            var entities = await Controller.QueryAsync(predicate, orderBy, pageIndex, pageSize).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        /// <summary>
        /// Filters a subset of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public virtual async Task<TModel[]> QueryAsync(string predicate, string orderBy, int pageIndex, int pageSize, params string[] includeItems)
        {
            var entities = await Controller.QueryAsync(predicate, orderBy, pageIndex, pageSize, includeItems).ConfigureAwait(false);

            return entities.Select(e => ToModel(e)).ToArray();
        }
        #endregion Queries

        #region Insert
        /// <summary>
        /// The element is being tracked by the context but does not yet exist in the repository. 
        /// </summary>
        /// <param name="element">The element which is to be inserted.</param>
        /// <returns>The inserted element.</returns>
        public virtual async Task<TModel> InsertAsync(TModel model)
        {
            var entity = await Controller.InsertAsync(ToEntity(model)).ConfigureAwait(false);

            return ToModel(entity);
        }
        /// <summary>
        /// The elements are being tracked by the context but does not yet exist in the repository. 
        /// </summary>
        /// <param name="elements">The elements which are to be inserted.</param>
        /// <returns>The inserted elements.</returns>
        public virtual async Task<IEnumerable<TModel>> InsertAsync(IEnumerable<TModel> models)
        {
            var entities = await Controller.InsertAsync(models.Select(f => ToEntity(f))).ConfigureAwait(false);

            return entities.Select(e => ToModel(e));
        }
        #endregion Insert

        #region Update
        /// <summary>
        /// The element is being tracked by the context and exists in the repository, and some or all of its property values have been modified.
        /// </summary>
        /// <param name="element">The element which is to be updated.</param>
        /// <returns>The the modified element.</returns>
        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            var entity = await Controller.GetByIdAsync(model.Id).ConfigureAwait(false);

            _ = entity ?? throw new Exception("Entity not found.");

            entity.CopyFrom(model);
            entity = await Controller.UpdateAsync(entity).ConfigureAwait(false);

            return ToModel(entity);
        }
        /// <summary>
        /// The elements are being tracked by the context and exists in the repository, and some or all of its property values have been modified.
        /// </summary>
        /// <param name="elements">The elements which are to be updated.</param>
        /// <returns>The updated elements.</returns>
        public virtual async Task<IEnumerable<TModel>> UpdateAsync(IEnumerable<TModel> models)
        {
            var result = new List<TModel>();

            foreach (var model in models)
            {
                var updateModel = await UpdateAsync(model).ConfigureAwait(false);

                result.Add(updateModel);
            }
            return result;
        }
        #endregion Update

        #region Delete
        /// <summary>
        /// Removes the element from the repository with the appropriate idelement.
        /// </summary>
        /// <param name="id">The identification.</param>
        public virtual Task DeleteAsync(IdType id)
        {
            return Controller.DeleteAsync(id);
        }
        #endregion Delete

        #region SaveChanges
        /// <summary>
        /// Saves any changes in the underlying persistence.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.</returns>
        public Task<int> SaveChangesAsync()
        {
            return Controller.SaveChangesAsync();
        }
        #endregion SaveChanges
    }
}
//MdEnd
