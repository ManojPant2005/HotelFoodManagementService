using System.Text.Json.Serialization;

namespace HotelFoodManagementService.Data.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Recipe>? Recipes { get; set; }
    }
}
