//@CodeCopy
//MdStart
#if ACCOUNT_ON
using System.Globalization;

namespace QuickTemplate.AspMvc.Models.Account
{
    public partial class AccessRole : VersionModel
    {
#if !GUID_ON
        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        public Guid Guid { get; set; }
#endif
        public string Designation { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public static AccessRole Create(object source)
        {
            var result = new AccessRole();

            result.CopyFrom(source);
            return result;
        }
    }
}
#endif
//MdEnd
