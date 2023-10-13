﻿namespace ProductCatalog.API.DTOs
{
    public class UpdateProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string GeneralNote { get; set; }

        public string SpecialNote { get; set; }

        public Guid CategoryId { get; set; }
    }
}
