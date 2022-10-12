//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.AspMvc.Models.Account
{
    public partial class AccessRole : VersionModel
    {
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
