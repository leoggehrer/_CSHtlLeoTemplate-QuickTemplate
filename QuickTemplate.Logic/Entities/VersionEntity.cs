//@CodeCopy
//MdStart
namespace QuickTemplate.Logic.Entities
{
    public abstract partial class VersionEntity : Entity, Contracts.IVersionable
    {
#if ROWVERSION_ON
        /// <summary>
        /// Row version of the entity.
        /// </summary>
        [Timestamp]
        public byte[]? RowVersion { get; internal set; }
#endif
    }
}
//MdEnd
