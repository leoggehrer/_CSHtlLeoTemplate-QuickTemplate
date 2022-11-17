//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Models.Account
{
    using System;
    public partial class Identity : VersionObject
    {
        static Identity()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public Identity()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        new internal QuickTemplate.Logic.Entities.Account.SecureIdentity Source
        {
            get => (QuickTemplate.Logic.Entities.Account.SecureIdentity)(_source ??= new QuickTemplate.Logic.Entities.Account.SecureIdentity());
            set => _source = value;
        }
        public Guid Guid
        {
            get => Source.Guid;
            set => Source.Guid = value;
        }
        public System.String Name
        {
            get => Source.Name;
            set => Source.Name = value;
        }
        public System.String Email
        {
            get => Source.Email;
            set => Source.Email = value;
        }
        public System.Int32 TimeOutInMinutes
        {
            get => Source.TimeOutInMinutes;
            set => Source.TimeOutInMinutes = value;
        }
        public System.Int32 AccessFailedCount
        {
            get => Source.AccessFailedCount;
            set => Source.AccessFailedCount = value;
        }
        public QuickTemplate.Logic.Modules.Common.State State
        {
            get => Source.State;
            set => Source.State = value;
        }
        internal void CopyProperties(QuickTemplate.Logic.Entities.Account.SecureIdentity other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
                Guid = other.Guid;
                Name = other.Name;
                Email = other.Email;
                TimeOutInMinutes = other.TimeOutInMinutes;
                AccessFailedCount = other.AccessFailedCount;
                State = other.State;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QuickTemplate.Logic.Entities.Account.SecureIdentity other, ref bool handled);
        partial void AfterCopyProperties(QuickTemplate.Logic.Entities.Account.SecureIdentity other);
        internal void CopyProperties(QuickTemplate.Logic.Models.Account.Identity other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
                Guid = other.Guid;
                Name = other.Name;
                Email = other.Email;
                TimeOutInMinutes = other.TimeOutInMinutes;
                AccessFailedCount = other.AccessFailedCount;
                State = other.State;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QuickTemplate.Logic.Models.Account.Identity other, ref bool handled);
        partial void AfterCopyProperties(QuickTemplate.Logic.Models.Account.Identity other);
        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is Models.Account.Identity other)
            {
                result = IsEqualsWith(this.GetType().Name, other.GetType().Name) && Id == other.Id;
            }
            return result;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Guid, Name, Email, TimeOutInMinutes, AccessFailedCount, State);
        }
        public static QuickTemplate.Logic.Models.Account.Identity Create()
        {
            BeforeCreate();
            var result = new QuickTemplate.Logic.Models.Account.Identity();
            AfterCreate(result);
            return result;
        }
        public static QuickTemplate.Logic.Models.Account.Identity Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new QuickTemplate.Logic.Models.Account.Identity();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static QuickTemplate.Logic.Models.Account.Identity Create(QuickTemplate.Logic.Models.Account.Identity other)
        {
            BeforeCreate(other);
            var result = new QuickTemplate.Logic.Models.Account.Identity();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        internal static QuickTemplate.Logic.Models.Account.Identity Create(QuickTemplate.Logic.Entities.Account.SecureIdentity other)
        {
            BeforeCreate(other);
            var result = new QuickTemplate.Logic.Models.Account.Identity
            {
                Source = other
            };
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(QuickTemplate.Logic.Models.Account.Identity instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(QuickTemplate.Logic.Models.Account.Identity instance, object other);
        static partial void BeforeCreate(QuickTemplate.Logic.Models.Account.Identity other);
        static partial void AfterCreate(QuickTemplate.Logic.Models.Account.Identity instance, QuickTemplate.Logic.Models.Account.Identity other);
        static partial void BeforeCreate(QuickTemplate.Logic.Entities.Account.SecureIdentity other);
        static partial void AfterCreate(QuickTemplate.Logic.Models.Account.Identity instance, QuickTemplate.Logic.Entities.Account.SecureIdentity other);
    }
}
#endif
//MdEnd
