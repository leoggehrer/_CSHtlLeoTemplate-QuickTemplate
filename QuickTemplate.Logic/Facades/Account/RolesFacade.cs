//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Facades.Account
{
    using TModel = Models.Account.Role;
    using TEntity = Entities.Account.Role;
    using System;

    public sealed partial class RolesFacade : FacadeObject, Contracts.Account.IRolesAccess<TModel>
    {
        private Contracts.Account.IRolesAccess<TEntity> Controller => (ControllerObject as Contracts.Account.IRolesAccess<TEntity>)!;
        public string SessionToken 
        {
            set => Controller.SessionToken = value; 
        }
        public int MaxPageSize => Controller.MaxPageSize;

        public RolesFacade()
            : base(new Logic.Controllers.Account.RolesController())
        {
        }
        public RolesFacade(FacadeObject facadeObject)
            : base(new Logic.Controllers.Account.RolesController(facadeObject.ControllerObject))
        {

        }

        /// <summary>
        /// Converts the entity type to the facade type.
        /// </summary>
        /// <param name="entity">Entity type</param>
        /// <returns>The facade type</returns>
        internal TModel ToModel(TEntity entity)
        {
            var handled = false;
            var result = default(TModel);

            BeforeToModel(entity, ref result, ref handled);
            if (handled == false || result == null)
            {

                result = new TModel
                {
                    Source = entity
                };
            }
            AfterToModel(entity, result);
            return result;
        }
        partial void BeforeToModel(TEntity entity, ref TModel? model, ref bool handled);
        partial void AfterToModel(TEntity entity, TModel model);
        /// <summary>
        /// Converts the model type to the entity type.
        /// </summary>
        /// <param name="model">Model type</param>
        /// <returns>The entity type</returns>
        internal TEntity ToEntity(TModel model)
        {
            var handled = false;
            var result = default(TEntity);

            BeforeToEntity(model, ref result, ref handled);
            if (handled == false || result == null)
            {
                result = model.Source as TEntity;
            }
            AfterToEntity(model, result!);
            return result!;
        }
        partial void BeforeToEntity(TModel model, ref TEntity? entity, ref bool handled);
        partial void AfterToEntity(TModel model, TEntity entity);

        /// <summary>
        /// Gets the number of quantity in the collection.
        /// </summary>
        /// <returns>Number of elements in the collection.</returns>
        public Task<int> CountAsync()
        {
            return Controller.CountAsync();
        }
        /// <summary>
        /// Returns the number of quantity in the collection based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <returns>Number of entities in the collection.</returns>
        public Task<int> CountAsync(string predicate)
        {
            return Controller.CountAsync(predicate);
        }
        /// <summary>
        /// Returns the number of quantity in the collection based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Number of entities in the collection.</returns>
        public Task<int> CountAsync(string predicate, params string[] includeItems)
        {
            return Controller.CountAsync(predicate, includeItems);
        }

        /// <summary>
        /// Returns the element of type T with the identification of id.
        /// </summary>
        /// <param name="id">The identification.</param>
        /// <returns>The element of the type T with the corresponding identification.</returns>
        public async Task<TModel?> GetByIdAsync(int id)
        {
            var handled = false;
            var result = default(TModel);

            BeforeGetById(ref result, id, ref handled);
            if (handled == false || result == null)
            {
                var entity = await Controller.GetByIdAsync(id).ConfigureAwait(false);

                if (entity != null)
                    result = ToModel(entity);
            }
            AfterGetById(result);
            return result;
        }
        partial void BeforeGetById(ref TModel? model, int id, ref bool handled);
        partial void AfterGetById(TModel? model);
        /// <summary>
        /// Returns the element of type T with the identification of id.
        /// </summary>
        /// <param name="id">The identification.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>The element of the type T with the corresponding identification (with includes).</returns>
        public async Task<TModel?> GetByIdAsync(int id, params string[] includeItems)
        {
            var handled = false;
            var result = default(TModel);

            BeforeGetById(ref result, id, includeItems, ref handled);
            if (handled == false || result == null)
            {
                var entity = await Controller.GetByIdAsync(id, includeItems).ConfigureAwait(false);

                if (entity != null)
                    result = ToModel(entity);
            }
            AfterGetById(result, includeItems);
            return result;
        }
        partial void BeforeGetById(ref TModel? model, int id, string[] includeItems, ref bool handled);
        partial void AfterGetById(TModel? model, string[] includeItems);

        /// <summary>
        /// Returns all objects of the elements in the collection.
        /// </summary>
        /// <returns>All objects of the element collection.</returns>
        public async Task<TModel[]> GetAllAsync()
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeGetAll(ref result, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.GetAllAsync().ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterGetAll(result);
            return result;
        }
        partial void BeforeGetAll(ref TModel[]? models, ref bool handled);
        partial void AfterGetAll(TModel[] models);
        /// <summary>
        /// Returns all objects of the elements in the collection.
        /// </summary>
        /// <param name="includeItems">The include items</param>
        /// <returns>All objects of the element collection.</returns>
        public async Task<TModel[]> GetAllAsync(params string[] includeItems)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeGetAll(ref result, includeItems, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.GetAllAsync(includeItems).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterGetAll(result, includeItems);
            return result;
        }
        partial void BeforeGetAll(ref TModel[]? models, string[] includeItems, ref bool handled);
        partial void AfterGetAll(TModel[] models, string[] includeItems);
        /// <summary>
        /// Returns all elements in the collection.
        /// </summary>
        /// <param name="orderBy">Sorts the elements of a sequence according to a sort clause.</param>
        /// <returns>All interfaces of the element collection.</returns>
        public async Task<TModel[]> GetAllAsync(string orderBy)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeGetAll(ref result, orderBy, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.GetAllAsync(orderBy).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterGetAll(result, orderBy);
            return result;
        }
        partial void BeforeGetAll(ref TModel[]? models, string orderBy, ref bool handled);
        partial void AfterGetAll(TModel[] models, string orderBy);
        /// <summary>
        /// Returns all interfaces of the elements in the collection.
        /// </summary>
        /// <param name="includeItems">The include items</param>
        /// <returns>All interfaces of the element collection.</returns>
        public async Task<TModel[]> GetAllAsync(string orderBy, params string[] includeItems)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeGetAll(ref result, orderBy, includeItems, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.GetAllAsync(orderBy, includeItems).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterGetAll(result, orderBy, includeItems);
            return result;
        }
        partial void BeforeGetAll(ref TModel[]? models, string orderBy, string[] includeItems, ref bool handled);
        partial void AfterGetAll(TModel[] models, string orderBy, string[] includeItems);

        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public async Task<TModel[]> GetPageListAsync(int pageIndex, int pageSize)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeGetPageList(ref result, pageIndex, pageSize, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterGetPageList(result, pageIndex, pageSize);
            return result;
        }
        partial void BeforeGetPageList(ref TModel[]? models, int pageIndex, int pageSize, ref bool handled);
        partial void AfterGetPageList(TModel[] models, int pageIndex, int pageSize);
        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public async Task<TModel[]> GetPageListAsync(int pageIndex, int pageSize, params string[] includeItems)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeGetPageList(ref result, pageIndex, pageSize, includeItems, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.GetPageListAsync(pageIndex, pageSize, includeItems).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterGetPageList(result, pageIndex, pageSize, includeItems);
            return result;
        }
        partial void BeforeGetPageList(ref TModel[]? models, int pageIndex, int pageSize, string[] includeItems, ref bool handled);
        partial void AfterGetPageList(TModel[] models, int pageIndex, int pageSize, string[] includeItems);
        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public async Task<TModel[]> GetPageListAsync(string orderBy, int pageIndex, int pageSize)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeGetPageList(ref result, orderBy, pageIndex, pageSize, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.GetPageListAsync(orderBy, pageIndex, pageSize).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterGetPageList(result, orderBy, pageIndex, pageSize);
            return result;
        }
        partial void BeforeGetPageList(ref TModel[]? models, string orderBy, int pageIndex, int pageSize, ref bool handled);
        partial void AfterGetPageList(TModel[] models, string orderBy, int pageIndex, int pageSize);
        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public async Task<TModel[]> GetPageListAsync(string orderBy, int pageIndex, int pageSize, params string[] includeItems)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeGetPageList(ref result, orderBy, pageIndex, pageSize, includeItems, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.GetPageListAsync(orderBy, pageIndex, pageSize, includeItems).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterGetPageList(result, orderBy, pageIndex, pageSize, includeItems);
            return result;
        }
        partial void BeforeGetPageList(ref TModel[]? models, string orderBy, int pageIndex, int pageSize, string[] includeItems, ref bool handled);
        partial void AfterGetPageList(TModel[] models, string orderBy, int pageIndex, int pageSize, string[] includeItems);

        /// <summary>
        /// Filters a sequence of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <returns>The filter result.</returns>
        public async Task<TModel[]> QueryAsync(string predicate)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeQuery(ref result, predicate, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.QueryAsync(predicate).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterQuery(result, predicate);
            return result;
        }
        partial void BeforeQuery(ref TModel[]? models, string predicate, ref bool handled);
        partial void AfterQuery(TModel[] models, string predicate);
        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public async Task<TModel[]> QueryAsync(string predicate, params string[] includeItems)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeQuery(ref result, predicate, includeItems, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.QueryAsync(predicate, includeItems).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterQuery(result, predicate, includeItems);
            return result;
        }
        partial void BeforeQuery(ref TModel[]? models, string predicate, string[] includeItems, ref bool handled);
        partial void AfterQuery(TModel[] models, string predicate, string[] includeItems);
        /// <summary>
        /// Filters a sequence of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <returns>The filter result.</returns>
        public async Task<TModel[]> QueryAsync(string predicate, string orderBy)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeQuery(ref result, predicate, orderBy, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.QueryAsync(predicate, orderBy).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterQuery(result, predicate, orderBy);
            return result;
        }
        partial void BeforeQuery(ref TModel[]? models, string predicate, string orderBy, ref bool handled);
        partial void AfterQuery(TModel[] models, string predicate, string orderBy);
        /// <summary>
        /// Filters a subset of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public async Task<TModel[]> QueryAsync(string predicate, int pageIndex, int pageSize)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeQuery(ref result, predicate, pageIndex, pageSize, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.QueryAsync(predicate, pageIndex, pageSize).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterQuery(result, predicate, pageIndex, pageSize);
            return result;
        }
        partial void BeforeQuery(ref TModel[]? models, string predicate, int pageIndex, int pageSize, ref bool handled);
        partial void AfterQuery(TModel[] models, string predicate, int pageIndex, int pageSize);
        /// <summary>
        /// Filters a subset of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public async Task<TModel[]> QueryAsync(string predicate, int pageIndex, int pageSize, params string[] includeItems)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeQuery(ref result, predicate, pageIndex, pageSize, includeItems, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.QueryAsync(predicate, pageIndex, pageSize, includeItems).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterQuery(result, predicate, pageIndex, pageSize, includeItems);
            return result;
        }
        partial void BeforeQuery(ref TModel[]? models, string predicate, int pageIndex, int pageSize, string[] includeItems, ref bool handled);
        partial void AfterQuery(TModel[] models, string predicate, int pageIndex, int pageSize, string[] includeItems);
        /// <summary>
        /// Filters a subset of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public async Task<TModel[]> QueryAsync(string predicate, string orderBy, int pageIndex, int pageSize)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeQuery(ref result, predicate, orderBy, pageIndex, pageSize, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.QueryAsync(predicate, orderBy, pageIndex, pageSize).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterQuery(result, predicate, orderBy, pageIndex, pageSize);
            return result;
        }
        partial void BeforeQuery(ref TModel[]? models, string predicate, string orderBy, int pageIndex, int pageSize, ref bool handled);
        partial void AfterQuery(TModel[] models, string predicate, string orderBy, int pageIndex, int pageSize);
        /// <summary>
        /// Filters a subset of elements based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <param name="orderBy">Sorts the elements of a sequence in order according to a key.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <param name="includeItems">The include items</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        public async Task<TModel[]> QueryAsync(string predicate, string orderBy, int pageIndex, int pageSize, params string[] includeItems)
        {
            var handled = false;
            var result = default(TModel[]);

            BeforeQuery(ref result, predicate, orderBy, pageIndex, pageSize, includeItems, ref handled);
            if (handled == false || result == null)
            {
                result = (await Controller.QueryAsync(predicate, orderBy, pageIndex, pageSize, includeItems).ConfigureAwait(false)).Select(e => ToModel(e)).ToArray();
            }
            AfterQuery(result, predicate, orderBy, pageIndex, pageSize, includeItems);
            return result;
        }
        partial void BeforeQuery(ref TModel[]? models, string predicate, string orderBy, int pageIndex, int pageSize, string[] includeItems, ref bool handled);
        partial void AfterQuery(TModel[] models, string predicate, string orderBy, int pageIndex, int pageSize, string[] includeItems);

        /// <summary>
        /// The element is being tracked by the context but does not yet exist in the repository. 
        /// </summary>
        /// <param name="element">The element which is to be inserted.</param>
        /// <returns>The inserted element.</returns>
        public async Task<TModel> InsertAsync(TModel element)
        {
            var handled = false;
            var entity = ToEntity(element);
            var result = default(TModel);

            BeforeInsert(ref result, entity, ref handled);
            if (handled == false || result == null)
            {
                entity = await Controller.InsertAsync(entity).ConfigureAwait(false);
                result = ToModel(entity);
            }
            AfterInsert(result, entity);
            return result;
        }
        partial void BeforeInsert(ref TModel? model, TEntity entity, ref bool handled);
        partial void AfterInsert(TModel model, TEntity entity);
        /// <summary>
        /// The elements are being tracked by the context and exists in the repository, and some or all of its property values have been modified.
        /// </summary>
        /// <param name="elements">The elements which are to be updated.</param>
        /// <returns>The updated elements.</returns>
        public async Task<IEnumerable<TModel>> InsertAsync(IEnumerable<TModel> elements)
        {
            var handled = false;
            var entities = elements.Select(e => ToEntity(e));
            var result = default(TModel[]);

            BeforeInsert(ref result, entities, ref handled);
            if (handled == false || result == null)
            {
                entities = await Controller.InsertAsync(entities).ConfigureAwait(false);
                result = entities.Select(e => ToModel(e)).ToArray();
            }
            AfterInsert(result, entities);
            return result;
        }
        partial void BeforeInsert(ref TModel[]? models, IEnumerable<TEntity> entities, ref bool handled);
        partial void AfterInsert(TModel[] models, IEnumerable<TEntity> entities);

        /// <summary>
        /// The element is being tracked by the context and exists in the repository, and some or all of its property values have been modified.
        /// </summary>
        /// <param name="element">The element which is to be updated.</param>
        /// <returns>The the modified element.</returns>
        public async Task<TModel> UpdateAsync(TModel element)
        {
            var handled = false;
            var entity = ToEntity(element);
            var result = default(TModel);

            BeforeUpdate(ref result, entity, ref handled);
            if (handled == false || result == null)
            {
                entity = await Controller.UpdateAsync(entity).ConfigureAwait(false);
                result = ToModel(entity);
            }
            AfterUpdate(result, entity);
            return result;
        }
        partial void BeforeUpdate(ref TModel? model, TEntity entity, ref bool handled);
        partial void AfterUpdate(TModel model, TEntity entity);

        /// <summary>
        /// The elements are being tracked by the context and exists in the repository, and some or all of its property values have been modified.
        /// </summary>
        /// <param name="elements">The elements which are to be updated.</param>
        /// <returns>The updated elements.</returns>
        public async Task<IEnumerable<TModel>> UpdateAsync(IEnumerable<TModel> elements)
        {
            var handled = false;
            var entities = elements.Select(e => ToEntity(e));
            var result = default(TModel[]);

            BeforeUpdate(ref result, entities, ref handled);
            if (handled == false || result == null)
            {
                entities = await Controller.UpdateAsync(entities).ConfigureAwait(false);
                result = entities.Select(e => ToModel(e)).ToArray();
            }
            AfterUpdate(result, entities);
            return result;
        }
        partial void BeforeUpdate(ref TModel[]? models, IEnumerable<TEntity> entities, ref bool handled);
        partial void AfterUpdate(TModel[] models, IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes the element from the repository with the appropriate idelement.
        /// </summary>
        /// <param name="id">The identification.</param>
        public async Task DeleteAsync(int id)
        {
            var handled = false;

            BeforeDelete(id, ref handled);
            if (handled == false)
            {
                await Controller.DeleteAsync(id).ConfigureAwait(false);
            }
            AfterDelete(id);
        }
        partial void BeforeDelete(int id, ref bool handled);
        partial void AfterDelete(int id);

        /// <summary>
        /// Saves any changes in the underlying persistence.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.</returns>
        public async Task<int> SaveChangesAsync()
        {
            var handled = false;
            var result = 0;

            BeforeSaveChanges(ref result, ref handled);
            if (handled == false)
            {
                result = await Controller.SaveChangesAsync().ConfigureAwait(false);
            }
            AfterSaveChanges(result);
            return result;
        }
        partial void BeforeSaveChanges(ref int count, ref bool handled);
        partial void AfterSaveChanges(int count);
    }
}
#endif
//MdEnd
