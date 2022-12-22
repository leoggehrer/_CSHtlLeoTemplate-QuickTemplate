//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Facades.Account
{
    using TOutModel = Models.Account.Identity;
    public sealed partial class IdentitiesFacade : ControllerFacade<TOutModel>, Contracts.Account.IIdentitiesAccess<TOutModel>
    {
        public IdentitiesFacade()
            : base(new Controllers.Account.IdentitiesController())
        {
        }
        public IdentitiesFacade(FacadeObject facadeObject)
            : base(new Controllers.Account.IdentitiesController(facadeObject.ControllerObject))
        {

        }
    }
}
#endif
//MdEnd
