using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Required for [NotMapped]
using HONORE_API_MAIN.Constants; // Required to access AppConstants

namespace HONORE_API_MAIN.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; } // Store price as decimal for currency

        public double Rating { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public string MainImageUrl { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string About { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Ingredients { get; set; } = string.Empty;

        [MaxLength(500)]
        public string ShapeAndSize { get; set; } = string.Empty;

        [MaxLength(500)]
        public string ShelfLife { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string HowToUse { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? BreadSubcategory { get; set; }

        [MaxLength(50)]
        public string Weight { get; set; } = string.Empty;

        public FoodType FoodType { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        // NEW: Property to return the FoodType icon URL (not mapped to DB)
        [NotMapped]
        public string FoodTypeIconUrl
        {
            get
            {
                // Return the appropriate URL based on the FoodType enum value
                return FoodType switch
                {
                    FoodType.Veg => AppConstants.VEG_ICON_URL,
                    FoodType.SemiVeg => AppConstants.SEMI_VEG_ICON_URL,
                    _ => string.Empty // Default case, or throw an exception if an unknown FoodType is encountered
                };
            }
        }
    }
}