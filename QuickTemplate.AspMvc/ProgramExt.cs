//@CodeCopy
//MdStart
namespace QuickTemplate.AspMvc
{
    /// <summary>
    /// Extension Program
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// Services can be added using this method.
        /// </summary>
        /// <param name="builder">The builder</param>
        public static void BeforeBuild(WebApplicationBuilder builder)
        {
#if ACCOUNT_ON
            builder.Services.AddTransient<QuickTemplate.Logic.Contracts.Account.IIdentitiesAccess<QuickTemplate.Logic.Models.Account.Identity>, QuickTemplate.Logic.Facades.Account.IdentitiesFacade>();
            builder.Services.AddTransient<QuickTemplate.Logic.Contracts.Account.IRolesAccess<QuickTemplate.Logic.Models.Account.Role>, QuickTemplate.Logic.Facades.Account.RolesFacade>();
            builder.Services.AddTransient<QuickTemplate.Logic.Contracts.Account.IUsersAccess<QuickTemplate.Logic.Models.Account.User>, QuickTemplate.Logic.Facades.Account.UsersFacade>();
#endif
            AddServices(builder);
        }
        /// <summary>
        /// Configures can be added using this method.
        /// </summary>
        /// <param name="app"></param>
        public static void AfterBuild(WebApplication app)
        {
            AddConfigures(app);
        }
        static partial void AddServices(WebApplicationBuilder builder);
        static partial void AddConfigures(WebApplication app);
    }
}
//MdEnd
