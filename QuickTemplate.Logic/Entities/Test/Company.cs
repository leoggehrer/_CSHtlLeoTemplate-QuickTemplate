//@Ignore
namespace QuickTemplate.Logic.Entities.Test
{
    [Table("Companies", Schema = "test")]
    [Index(nameof(Name), IsUnique = true)]
    internal class Company : VersionEntity
    {
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(256)]
        public string Address { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("CompanyId")]
        public List<Customer> Customers { get; set; } = new();
    }
}
