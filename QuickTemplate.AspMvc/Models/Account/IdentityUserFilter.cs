//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QuickTemplate.AspMvc.Models.Account
{
    public partial class IdentityUserFilter : Models.View.IFilterModel
    {
        static IdentityUserFilter()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public IdentityUserFilter()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public System.Int32? IdentityId
        {
            get;
            set;
        }
        public System.String? Firstname
        {
            get;
            set;
        }
        public System.String? Lastname
        {
            get;
            set;
        }
        public bool HasEntityValue => IdentityId != null || Firstname != null || Lastname != null;
        private bool show = true;
        public bool Show => show;
        public string CreateEntityPredicate()
        {
            var result = new System.Text.StringBuilder();
            if (IdentityId != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(IdentityId != null && IdentityId == {IdentityId})");
            }
            if (Firstname != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(Firstname != null && Firstname.Contains(\"{Firstname}\"))");
            }
            if (Lastname != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" || ");
                }
                result.Append($"(Lastname != null && Lastname.Contains(\"{Lastname}\"))");
            }
            return result.ToString();
        }
        public override string ToString()
        {
            return $"IdentityId: {(IdentityId != null ? IdentityId : "---")} Firstname: {(Firstname ?? "---")} Lastname: {(Lastname ?? "---")} ";
        }
    }
}
#endif
//MdEnd
