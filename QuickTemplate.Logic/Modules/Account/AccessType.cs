//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.Logic.Modules.Account
{
    public enum AccessType
    {
        Identity = 1,
        Role = 2 * Identity,
        Entity = 2 * Role,
        All = Identity + Role + Entity,
    }
}
#endif
//MdEnd
