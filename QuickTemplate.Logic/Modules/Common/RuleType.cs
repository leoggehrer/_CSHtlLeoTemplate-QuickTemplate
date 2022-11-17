//@CodeCopy
//MdStart

namespace QuickTemplate.Logic.Modules.Common
{
    public enum RuleType
    {
        EntityType = 1,
        Property = 2 * EntityType,
        EntityValue = 2 * Property,
        PropertyValue = 2 * EntityValue,
    }
}
//MdEnd
