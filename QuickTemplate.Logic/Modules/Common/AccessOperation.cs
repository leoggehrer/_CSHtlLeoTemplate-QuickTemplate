//@CodeCopy
//MdStart
namespace QuickTemplate.Logic.Modules.Common
{
    public enum AccessOperation
    {
        None = 0,
        Create = 1,
        Read = 2 * Create,
        Update = 2 * Read,
        Delete = 2 * Update,
        Display = 2 * Delete,

        CRUD = Create + Read + Update + Delete,
    }
}
//MdEnd
