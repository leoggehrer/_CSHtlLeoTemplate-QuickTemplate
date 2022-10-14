//@CodeCopy
//MdStart
namespace TemplateCodeGenerator.Logic.Common
{
    [Flags]
    public enum ItemType : ulong
    {
        Enum = 1,
        Entity = 2 * Enum,
        Model = 2 * Entity,
        EditModel = 2 * Model,
        FilterModel = 2 * EditModel,

        AccessContract = 2 * FilterModel,
        ServiceContract = 2 * AccessContract,

        Property = 2 * ServiceContract,

        DbContext = 2 * Property,

        Controller = 2 * DbContext,
        Service = 2 * Controller,
        Facade = 2 * Service,

        Factory = 2 * Facade,
        FactoryControllerMethode = 2 * Factory,
        FactoryFacadeMethode = 2 * FactoryControllerMethode,

        AddServices = 2 * FactoryFacadeMethode,

        View = 2 * AddServices,
        ViewItem = 2 * View,

        TypeScriptEnum = 2 * ViewItem,
        TypeScriptModel = 2 * TypeScriptEnum,
        TypeScriptService = 2 * TypeScriptModel,
    }
}
//MdEnd
