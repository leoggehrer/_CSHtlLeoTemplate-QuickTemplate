//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QuickTemplate.AspMvc.Models.Account
{
    public partial class AccessRuleFilter : Models.View.IFilterModel
    {
        public string? EntityType { get; set; }
        public string? RelationshipEntityType { get; set; }

        public bool HasEntityValue => EntityType != null || RelationshipEntityType != null;
        private bool show = true;
        public bool Show => show;
        public string CreateEntityPredicate()
        {
            var result = new System.Text.StringBuilder();

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
            return result.ToString();
        }
        public override string ToString()
        {
            System.Text.StringBuilder sb = new();
            if (string.IsNullOrEmpty(EntityType) == false)
            {
                sb.Append($"EntityType: {EntityType} ");
            }
            if (string.IsNullOrEmpty(RelationshipEntityType) == false)
            {
                sb.Append($"RelationshipEntityType: {RelationshipEntityType} ");
            }
            return sb.ToString();
        }
    }
}
#endif
//MdEnd
