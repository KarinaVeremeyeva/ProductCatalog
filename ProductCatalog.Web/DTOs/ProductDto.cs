﻿namespace ProductCatalog.Web.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string GeneralNote { get; set; }

        public string SpecialNote { get; set; }

        public CategoryDto Category { get; set; }
    }
}