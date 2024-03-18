using HotelFoodManagementService.Components.Models;

namespace HotelFoodManagementService.Service.Contracts
{
    public interface ICategoryService
    {
        // Category
        Task<int> AddOrUpdateCategoryAsync(CategoryModel categoryModel);
        Task<CategoryModel> GetCategoryByIdAsync(int id);
        Task<List<CategoryModel>> GetCategoriesAsync();
        Task<int> DeleteCategoryAsync(int id);
    }
}
