//@CodeCopy
//MdStart
namespace QuickTemplate.Logic.Entities
{
#if ACCOUNT_ON
    using QuickTemplate.Logic.Entities.Account;
#endif
    public abstract partial class Entity : EntityObject
    {
#if GUID_ON
        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        public Guid Guid { get; internal set; }
#endif

#if CREATED_ON
        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        public DateTime CreatedOn { get; internal set; }
#endif

#if MODIFIED_ON
        /// <summary>
        /// Gets or sets the last modified time.
        /// </summary>
        public DateTime? ModifiedOn { get; internal set; }
#endif

#if ACCOUNT_ON && CREATEDBY_ON
        /// <summary>
        /// Gets or sets the owner (Identity) reference.
        /// </summary>
        [Column("CreatedById")]
        [ForeignKey(nameof(Identity))]
        public IdType? IdentityId_CreatedBy { get; internal set; }
#endif

#if ACCOUNT_ON && MODIFIEDBY_ON
        /// <summary>
        /// Gets or sets the reference of the user (Identity) who made the last change.
        /// </summary>
        [Column("ModifiedById")]
        [ForeignKey(nameof(Identity))]
        public IdType? IdentityId_ModifiedBy { get; internal set; }
#endif
    }
}
//MdEnd
