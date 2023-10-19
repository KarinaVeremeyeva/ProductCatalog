using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Web.ViewModels
{
    public class UpdateProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public string GeneralNote { get; set; }

        public string SpecialNote { get; set; }

        public Guid CategoryId { get; set; }
    }
}
