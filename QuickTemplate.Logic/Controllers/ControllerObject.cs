//@CodeCopy
//MdStart
using QuickTemplate.Logic.DataContext;

namespace QuickTemplate.Logic.Controllers
{
#if ACCOUNT_ON
    using QuickTemplate.Logic.Modules.Security;
#endif
    public abstract partial class ControllerObject : IDisposable
    {
        static ControllerObject()
        {
            BeforeClassInitialize();
            AfterClassInitialize();
        }
        static partial void BeforeClassInitialize();
        static partial void AfterClassInitialize();

        private readonly bool contextOwner;
        internal ProjectDbContext? Context { get; private set; }
#if ACCOUNT_ON
        #region SessionToken
        protected event EventHandler ChangedSessionToken;

        private string? sessionToken;

        /// <summary>
        /// Sets the session token.
        /// </summary>
        public string SessionToken
        {
            internal get => sessionToken ?? string.Empty;
            set
            {
                sessionToken = value;
                ChangedSessionToken?.Invoke(this, EventArgs.Empty);
            }
        }

        private void HandleChangedSessionToken(object source, EventArgs e)
        {
            var handled = false;

            BeforeHandleManagedMembers(ref handled);

            if (handled == false)
            {
            }
            AfterHandleManagedMembers();
        }
        partial void BeforeHandleManagedMembers(ref bool handled);
        partial void AfterHandleManagedMembers();
        #endregion SessionToken
#endif

        #region Instance-Constructors
        internal ControllerObject(ProjectDbContext context)
        {
            Constructing();
            contextOwner = true;
            Context = context;
#if ACCOUNT_ON
            ChangedSessionToken += HandleChangedSessionToken!;
#endif
            Constructed();
        }
        internal ControllerObject(ControllerObject other)
        {
            Constructing();
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            if (other.Context == null)
                throw new Modules.Exceptions.LogicException("The context from the other controller must not be null.");

            contextOwner = false;
            Context = other.Context;
#if ACCOUNT_ON
            SessionToken = other.SessionToken;
            ChangedSessionToken += HandleChangedSessionToken!;
#endif
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        #endregion Instance-Constructors

#if ACCOUNT_ON
        protected virtual Task CheckAuthorizationAsync(Type subjectType, string action)
        {
            return Authorization.CheckAuthorizationAsync(SessionToken, subjectType, action, string.Empty);
        }
        protected virtual Task CheckAuthorizationAsync(Type subjectType, string action, string infoData)
        {
            return Authorization.CheckAuthorizationAsync(SessionToken, subjectType, action, infoData);
        }

        protected virtual Task CheckAuthorizationAsync(Type subjectType, string action, params string[] roles)
        {
            return Authorization.CheckAuthorizationAsync(SessionToken, subjectType, action, string.Empty, roles);
        }
        protected virtual Task CheckAuthorizationAsync(Type subjectType, string action, string infoData, params string[] roles)
        {
            return Authorization.CheckAuthorizationAsync(SessionToken, subjectType, action, infoData, roles);
        }
#endif

        #region Dispose pattern
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (contextOwner)
                    {
                        Context?.Dispose();
                    }
                    Context = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion Dispose pattern
    }
}
//MdEnd
