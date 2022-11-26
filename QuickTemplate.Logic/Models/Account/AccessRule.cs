//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.Logic.Models.Account
{
    using QuickTemplate.Logic.Modules.Account;
    using TEntity = Entities.Account.AccessRule;
    using TModel = Models.Account.AccessRule;
    public partial class AccessRule : VersionObject
    {
        static AccessRule()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public AccessRule()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        new internal TEntity Source
        {
            get => (TEntity)(_source ??= new TEntity());
            set => _source = value;
        }
        public RuleType Type
        {
            get => Source.Type;
            set => Source.Type = value;
        }
        public System.String EntityType
        {
            get => Source.EntityType;
            set => Source.EntityType = value;
        }
        public System.String? RelationshipEntityType
        {
            get => Source.RelationshipEntityType;
            set => Source.RelationshipEntityType = value;
        }
        public System.String? PropertyName
        {
            get => Source.PropertyName;
            set => Source.PropertyName = value;
        }
        public System.String? EntityValue
        {
            get => Source.EntityValue;
            set => Source.EntityValue = value;
        }
        public AccessType AccessType
        {
            get => Source.AccessType;
            set => Source.AccessType = value;
        }
        public System.String? AccessValue
        {
            get => Source.AccessValue;
            set => Source.AccessValue = value;
        }

        public bool Creatable
        {
            get => Source.Creatable;
            set => Source.Creatable = value;
        }
        public bool Readable
        {
            get => Source.Readable;
            set => Source.Readable = value;
        }
        public bool Updatable
        {
            get => Source.Updatable;
            set => Source.Updatable = value;
        }
        public bool Deletable
        {
            get => Source.Deletable;
            set => Source.Deletable = value;
        }
        public bool Viewable
        {
            get => Source.Viewable;
            set => Source.Viewable = value;
        }

        internal void CopyProperties(TEntity other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
#if ROWVERSION_ON
                RowVersion = other.RowVersion;
#endif
                Type = other.Type;
                EntityType = other.EntityType;
                RelationshipEntityType = other.RelationshipEntityType;
                PropertyName = other.PropertyName;
                EntityValue = other.EntityValue;
                AccessType = other.AccessType;
                AccessValue = other.AccessValue;
                Creatable = other.Creatable;
                Readable = other.Readable;
                Updatable = other.Updatable;
                Deletable = other.Deletable;
                Viewable = other.Viewable;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(TEntity other, ref bool handled);
        partial void AfterCopyProperties(TEntity other);

        internal void CopyProperties(TModel other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
#if ROWVERSION_ON
                RowVersion = other.RowVersion;
#endif
                Type = other.Type;
                EntityType = other.EntityType;
                RelationshipEntityType = other.RelationshipEntityType;
                PropertyName = other.PropertyName;
                EntityValue = other.EntityValue;
                AccessType = other.AccessType;
                AccessValue = other.AccessValue;
                Creatable = other.Creatable;
                Readable = other.Readable;
                Updatable = other.Updatable;
                Deletable = other.Deletable;
                Viewable = other.Viewable;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(TModel other, ref bool handled);
        partial void AfterCopyProperties(TModel other);

        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is AccessRule other)
            {
                result = IsEqualsWith(GetType().Name, other.GetType().Name) && Id == other.Id;
            }
            return result;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Type, EntityType, RelationshipEntityType, PropertyName, EntityValue, AccessType, AccessValue, Id);
        }

        public static TModel Create()
        {
            BeforeCreate();
            var result = new AccessRule();
            AfterCreate(result);
            return result;
        }
        public static TModel Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new AccessRule();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static TModel Create(TModel other)
        {
            BeforeCreate(other);
            var result = new AccessRule();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        internal static TModel Create(TEntity other)
        {
            BeforeCreate(other);
            var result = new AccessRule();
            result.Source = other;
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(TModel instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(TModel instance, object other);
        static partial void BeforeCreate(TModel other);
        static partial void AfterCreate(TModel instance, TModel other);
        static partial void BeforeCreate(TEntity other);
        static partial void AfterCreate(TModel instance, TEntity other);
    }
}
#endif
//MdEnd
