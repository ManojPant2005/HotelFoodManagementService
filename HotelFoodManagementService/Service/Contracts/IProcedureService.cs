using HotelFoodManagementService.Components.Models;

namespace HotelFoodManagementService.Service.Contracts
{
    public interface IProcedureService
    {
        // Procedure
        Task<int> AddOrUpdateStepAsync(StepModels stepModel);
        Task<StepModels> GetStepByIdAsync(int id);
        Task<List<StepModels>> GetStepByRecipeIdAsync(int recipeId);
        Task<List<StepModels>> GetStepsAsync();
        Task<int> DeleteStepAsync(int id);
    }
}
