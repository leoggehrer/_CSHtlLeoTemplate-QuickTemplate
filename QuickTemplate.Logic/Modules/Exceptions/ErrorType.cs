//@CodeCopy
//MdStart
namespace QuickTemplate.Logic.Modules.Exceptions
{
    public enum ErrorType : int
    {
#if ACCOUNT_ON
        InitAppAccess,
        InvalidAccount,
        InvalidIdentityName,
        InvalidPasswordSyntax,

        InvalidToken,
        InvalidSessionToken,
        InvalidJsonWebToken,

        InvalidEmail,
        InvalidPassword,
        NotLogedIn,
        NotAuthorized,
        AuthorizationTimeOut,

#if ACCESSRULES_ON
        InvalidAccessRuleEntityValue,
        InvalidAccessRuleAccessValue,
        InvalidAccessRuleAlreadyExits,

        AccessRuleViolationCanNotCreated,
        AccessRuleViolationCanNotRead,
        AccessRuleViolationCanNotChanged,
        AccessRuleViolationCanNotDeleted,
#endif
#endif
        InvalidId,
        InvalidPageSize,

        InvalidEntityInsert,
        InvalidEntityUpdate,
        InvalidEntityContent,

        InvalidControllerType,
        InvalidControllerObject,

        InvalidFacadeType,
        InvalidFacadeObject,

        InvalidOperation,
    }
}
//MdEnd
