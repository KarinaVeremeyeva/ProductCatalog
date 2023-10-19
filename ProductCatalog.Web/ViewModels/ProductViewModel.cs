namespace ProductCatalog.Web.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string GeneralNote { get; set; }

        public string SpecialNote { get; set; }

        public CategoryViewModel Category { get; set; }
    }
}
