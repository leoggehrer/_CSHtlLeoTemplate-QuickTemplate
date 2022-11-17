//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.WebApi.Models.Account
{
    using QuickTemplate.Logic.Modules.Common;
    /// <summary>
    /// This model represents an account identity.
    /// </summary>
    public class AccessIdentity : VersionModel
    {
#if !GUID_ON
        /// <summary>
        /// Gets or sets the property data.
        /// </summary>
        public string Guid { get; set; } = string.Empty;
#endif
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
        /// Creates an instance of type AccessIdentity.
        /// </summary>
        /// <param name="source">The object from which the instance is created.</param>
        /// <returns>The created instance.</returns>
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
