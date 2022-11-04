//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.Logic.Models.Account
{
    using System;
    public partial class Role : VersionModel
    {
        static Role()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public Role()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        new internal QuickTemplate.Logic.Entities.Account.Role Source
        {
            get => (QuickTemplate.Logic.Entities.Account.Role)(_source ??= new QuickTemplate.Logic.Entities.Account.Role());
            set => _source = value;
        }
        public System.String Designation
        {
            get => Source.Designation;
            set => Source.Designation = value;
        }
        public System.String? Description
        {
            get => Source.Description;
            set => Source.Description = value;
        }
        partial void BeforeCopyProperties(QuickTemplate.Logic.Entities.Account.Role other, ref bool handled);
        partial void AfterCopyProperties(QuickTemplate.Logic.Entities.Account.Role other);
        internal void CopyProperties(QuickTemplate.Logic.Models.Account.Role other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Designation = other.Designation;
                Description = other.Description;
                RowVersion = other.RowVersion;
                Id = other.Id;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QuickTemplate.Logic.Models.Account.Role other, ref bool handled);
        partial void AfterCopyProperties(QuickTemplate.Logic.Models.Account.Role other);
        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is Models.Account.Role other)
            {
                result = IsEqualsWith(RowVersion, other.RowVersion)
                && Id == other.Id;
            }
            return result;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Designation, Description, RowVersion, Id);
        }
        public static QuickTemplate.Logic.Models.Account.Role Create()
        {
            BeforeCreate();
            var result = new QuickTemplate.Logic.Models.Account.Role();
            AfterCreate(result);
            return result;
        }
        public static QuickTemplate.Logic.Models.Account.Role Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new QuickTemplate.Logic.Models.Account.Role();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static QuickTemplate.Logic.Models.Account.Role Create(QuickTemplate.Logic.Models.Account.Role other)
        {
            BeforeCreate(other);
            var result = new QuickTemplate.Logic.Models.Account.Role();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        internal static QuickTemplate.Logic.Models.Account.Role Create(QuickTemplate.Logic.Entities.Account.Role other)
        {
            BeforeCreate(other);
            var result = new QuickTemplate.Logic.Models.Account.Role();
            result.Source = other;
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(QuickTemplate.Logic.Models.Account.Role instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(QuickTemplate.Logic.Models.Account.Role instance, object other);
        static partial void BeforeCreate(QuickTemplate.Logic.Models.Account.Role other);
        static partial void AfterCreate(QuickTemplate.Logic.Models.Account.Role instance, QuickTemplate.Logic.Models.Account.Role other);
        static partial void BeforeCreate(QuickTemplate.Logic.Entities.Account.Role other);
        static partial void AfterCreate(QuickTemplate.Logic.Models.Account.Role instance, QuickTemplate.Logic.Entities.Account.Role other);
    }
}
#endif
//MdEnd
