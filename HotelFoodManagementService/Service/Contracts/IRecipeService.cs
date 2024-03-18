using HotelFoodManagementService.Components.Models;

namespace HotelFoodManagementService.Service.Contracts
{
    public interface IRecipeService
    {
        // Recipe
        Task<int> AddOrUpdateRecipeAsync(RecipeModel recipeModel);
        Task<RecipeModel> GetRecipeByIdAsync(int id);
        Task<List<RecipeModel>> GetRecipeByCategoryIdAsync(int categoryId);
        Task<List<RecipeModel>> GetRecipesAsync();
        Task<int> DeleteRecipeAsync(int id);
    }
}
