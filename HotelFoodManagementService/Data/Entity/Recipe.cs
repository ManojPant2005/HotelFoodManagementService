
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelFoodManagementService.Data.Entity
{
    public class Recipe
    {
        public int Id { get; set; }
        public string RecipeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Rank { get; set; }
        [NotMapped]
        public TimeOnly GeneralTimeNeeded { get; set; } = TimeOnly.MinValue;    
        public string GeneralImage { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        [JsonIgnore]
        public List<Step>? Procedures { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
