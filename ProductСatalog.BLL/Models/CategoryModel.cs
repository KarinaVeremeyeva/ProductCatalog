namespace ProductCatalog.BLL.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProductModel> Products { get; set; }
    }
}
