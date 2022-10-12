//@CodeCopy
//MdStart
#if ACCOUNT_ON && LOGGING_ON
using QuickTemplate.Logic.Entities.Logging;

namespace QuickTemplate.Logic.Controllers.Logging
{
    internal sealed partial class ActionLogsController : GenericController<ActionLog>
    {
        public ActionLogsController()
        {
        }

        public ActionLogsController(ControllerObject other) : base(other)
        {
        }
    }
}
#endif
//MdEnd
