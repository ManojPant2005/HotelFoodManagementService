using HotelFoodManagementService.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace HotelFoodManagementService.Components.Models
{
    public class RecipeModel
    { 
    public int Id { get; set; }
    [Required, Display(Name = "Recipe Name")]
    public string RecipeName { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public int Rank { get; set; } = 0;
    [Required, DataType(DataType.Time)]
    public TimeOnly GeneralTimeNeeded { get; set; }
    [Required, Display(Name = "Image")]
    public string GeneralImage { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public List<Step>? Procedures { get; set; }
    public Category? Category { get; set; }
    [Required]
    public int CategoryId { get; set; }
    }
}
