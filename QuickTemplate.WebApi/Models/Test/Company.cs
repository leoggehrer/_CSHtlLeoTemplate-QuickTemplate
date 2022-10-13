//@GeneratedCode
namespace QuickTemplate.WebApi.Models.Test
{
    using System;
    ///
    /// Generated by the generator
    ///
    public partial class Company
    {
        ///
        /// Generated by the generator
        ///
        static Company()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        ///
        /// Generated by the generator
        ///
        public Company()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Generated by the generator
        ///
        public System.String Name
        {
            get;
            set;
        }
        = string.Empty;
        ///
        /// Generated by the generator
        ///
        public System.String Address
        {
            get;
            set;
        }
        = string.Empty;
        ///
        /// Generated by the generator
        ///
        public System.Collections.Generic.List<QuickTemplate.WebApi.Models.Test.Customer> Customers
        {
            get;
            set;
        }
        = new();
        ///
        /// Generated by the generator
        ///
        public static QuickTemplate.WebApi.Models.Test.Company Create()
        {
            BeforeCreate();
            var result = new QuickTemplate.WebApi.Models.Test.Company();
            AfterCreate(result);
            return result;
        }
        ///
        /// Generated by the generator
        ///
        public static QuickTemplate.WebApi.Models.Test.Company Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new QuickTemplate.WebApi.Models.Test.Company();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(QuickTemplate.WebApi.Models.Test.Company instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(QuickTemplate.WebApi.Models.Test.Company instance, object other);
        ///
        /// Generated by the generator
        ///
        public void CopyProperties(QuickTemplate.WebApi.Models.Test.Company other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Name = other.Name;
                Address = other.Address;
                Customers = other.Customers;
                RowVersion = other.RowVersion;
                Id = other.Id;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QuickTemplate.WebApi.Models.Test.Company other, ref bool handled);
        partial void AfterCopyProperties(QuickTemplate.WebApi.Models.Test.Company other);
        ///
        /// Generated by the generator
        ///
        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is Models.Test.Company other)
            {
                result = IsEqualsWith(RowVersion, other.RowVersion)
                && Id == other.Id;
            }
            return result;
        }
        ///
        /// Generated by the generator
        ///
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Address, Customers, RowVersion, Id);
        }
    }
}