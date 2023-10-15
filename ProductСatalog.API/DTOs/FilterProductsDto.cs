namespace ProductCatalog.API.DTOs
{
    public class FilterProductsDto
    {
        public string? NamePart { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string? CategoryNamePart { get; set; }
    }
}
