//@Ignore
namespace QuickTemplate.Logic.Entities.Test
{
    [Table("Customers", Schema = "test")]
    internal class Customer : VersionEntity
    {
        [MaxLength(128)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(256)]
        public string LastName { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("CustomerId")]
        public List<Company> Companies { get; set; } = new();
    }
}
