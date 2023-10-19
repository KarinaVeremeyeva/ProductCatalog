namespace ProductCatalog.Web.DTOs
{
    public class FilterProductDto
    {
        public string? NamePart { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string? CategoryNamePart { get; set; }
    }
}
