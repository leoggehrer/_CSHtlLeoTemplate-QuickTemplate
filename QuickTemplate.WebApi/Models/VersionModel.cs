//@CodeCopy
//MdStart
using QuickTemplate.Logic.Contracts;
using System.ComponentModel.DataAnnotations;

namespace QuickTemplate.WebApi.Models
{
    /// <summary>
    /// The model with the version property.
    /// </summary>
    public abstract partial class VersionModel : ModelObject, IVersionable
    {
        /// <summary>
        /// Row version of the entity.
        /// </summary>
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
//MdEnd
