//@CodeCopy
//MdStart
#if ACCOUNT_ON
using QuickTemplate.Logic.Modules.Common;

namespace QuickTemplate.WebApi.Models.Account
{
    /// <summary>
    /// This model represents an account identity.
    /// </summary>
    public class AccessIdentityEdit
    {
        /// <summary>
        /// Gets or sets the property data.
        /// </summary>
        public string Guid { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the property data.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the property data.
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the property data.
        /// </summary>
        public int TimeOutInMinutes { get; set; } = 30;
        /// <summary>
        /// Gets or sets the property data.
        /// </summary>
        public int AccessFailedCount { get; set; }
        /// <summary>
        /// Gets or sets the property data.
        /// </summary>
        public State State { get; set; } = State.Active;

        /// <summary>
        /// Creates an instance of Identity and copies the properties of the same name from the object parameter. 
        /// </summary>
        /// <param name="source">The object to copy.</param>
        /// <returns></returns>
        public static AccessIdentity Create(object source)
        {
            var result = new AccessIdentity();

            result.CopyFrom(source);
            return result;
        }
    }
}
#endif
//MdEnd
