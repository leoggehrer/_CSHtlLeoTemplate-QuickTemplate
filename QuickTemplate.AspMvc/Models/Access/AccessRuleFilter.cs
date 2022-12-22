﻿//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.AspMvc.Models.Access
{
    using System;
    ///
    /// Generated by the generator
    ///
    public partial class AccessRuleFilter : Models.View.IFilterModel
    {
        ///
        /// Generated by the generator
        ///
        static AccessRuleFilter()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        ///
        /// Generated by the generator
        ///
        public AccessRuleFilter()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Generated by the generator
        ///
        public QuickTemplate.Logic.Modules.Access.RuleType? Type
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.String? EntityType
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.String? RelationshipEntityType
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.String? PropertyName
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.String? EntityValue
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public QuickTemplate.Logic.Modules.Access.AccessType? AccessType
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.String? AccessValue
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.Boolean? Creatable
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.Boolean? Readable
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.Boolean? Updatable
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.Boolean? Deletable
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public System.Boolean? Viewable
        {
            get;
            set;
        }
        ///
        /// Generated by the generator
        ///
        public bool HasEntityValue => Type != null || EntityType != null || RelationshipEntityType != null || PropertyName != null || EntityValue != null || AccessType != null || AccessValue != null || Creatable != null || Readable != null || Updatable != null || Deletable != null || Viewable != null;
        private bool show = true;
        ///
        /// Generated by the generator
        ///
        public bool Show => show;
        ///
        /// Generated by the generator
        ///
        public string CreateEntityPredicate()
        {
            var result = new System.Text.StringBuilder();
            if (Type != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                var ev = Convert.ChangeType(Type, typeof(int));
                result.Append($"(Type != null && Type =={ev})");
            }
            if (EntityType != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(EntityType != null && EntityType.Contains(\"{EntityType}\"))");
            }
            if (RelationshipEntityType != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(RelationshipEntityType != null && RelationshipEntityType.Contains(\"{RelationshipEntityType}\"))");
            }
            if (PropertyName != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(PropertyName != null && PropertyName.Contains(\"{PropertyName}\"))");
            }
            if (EntityValue != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(EntityValue != null && EntityValue.Contains(\"{EntityValue}\"))");
            }
            if (AccessType != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                var ev = Convert.ChangeType(AccessType, typeof(int));
                result.Append($"(AccessType != null && AccessType =={ev})");
            }
            if (AccessValue != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(AccessValue != null && AccessValue.Contains(\"{AccessValue}\"))");
            }
            if (Creatable != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(Creatable != null && Creatable == {Creatable})");
            }
            if (Readable != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(Readable != null && Readable == {Readable})");
            }
            if (Updatable != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(Updatable != null && Updatable == {Updatable})");
            }
            if (Deletable != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(Deletable != null && Deletable == {Deletable})");
            }
            if (Viewable != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(Viewable != null && Viewable == {Viewable})");
            }
            return result.ToString();
        }
        ///
        /// Generated by the generator
        ///
        public override string ToString()
        {
            System.Text.StringBuilder sb = new();
            if (Type != null)
            {
                sb.Append($"Type: {Type} ");
            }
            if (string.IsNullOrEmpty(EntityType) == false)
            {
                sb.Append($"EntityType: {EntityType} ");
            }
            if (string.IsNullOrEmpty(RelationshipEntityType) == false)
            {
                sb.Append($"RelationshipEntityType: {RelationshipEntityType} ");
            }
            if (string.IsNullOrEmpty(PropertyName) == false)
            {
                sb.Append($"PropertyName: {PropertyName} ");
            }
            if (string.IsNullOrEmpty(EntityValue) == false)
            {
                sb.Append($"EntityValue: {EntityValue} ");
            }
            if (AccessType != null)
            {
                sb.Append($"AccessType: {AccessType} ");
            }
            if (string.IsNullOrEmpty(AccessValue) == false)
            {
                sb.Append($"AccessValue: {AccessValue} ");
            }
            if (Creatable != null)
            {
                sb.Append($"Creatable: {Creatable} ");
            }
            if (Readable != null)
            {
                sb.Append($"Readable: {Readable} ");
            }
            if (Updatable != null)
            {
                sb.Append($"Updatable: {Updatable} ");
            }
            if (Deletable != null)
            {
                sb.Append($"Deletable: {Deletable} ");
            }
            if (Viewable != null)
            {
                sb.Append($"Viewable: {Viewable} ");
            }
            return sb.ToString();
        }
    }
}
#endif
//MdEnd
