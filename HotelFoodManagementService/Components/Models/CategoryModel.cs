using HotelFoodManagementService.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace HotelFoodManagementService.Components.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string CountryName { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;
        public List<Recipe>? Recipes { get; set; }
    }
}
