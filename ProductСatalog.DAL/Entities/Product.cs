namespace ProductCatalog.DAL.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string GeneralNote { get; set; }

        public string SpecialNote { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}