using HotelFoodManagementService.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace HotelFoodManagementService.Components.Models
{
    public class StepModels
    {
        [Required]
        public int Id { get; set; }
        [Required, Display(Name = "Procedure No")]
        public int ProcedureNo { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required, DataType(DataType.Time)]
        [Display(Name = "Time Needed")]
        public TimeOnly TimeNeeded { get; set; }

        public Recipe? Recipe { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}
