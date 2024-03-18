using AutoMapper;
using HotelFoodManagementService.Components.Models;
using HotelFoodManagementService.Data;
using HotelFoodManagementService.Data.Entity;
using HotelFoodManagementService.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelFoodManagementService.Service.Repositories
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        public CategoryService(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<int> AddOrUpdateCategoryAsync(CategoryModel categoryModel)
        {
            if (categoryModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var category = mapper.Map<Category>(categoryModel);

            if (categoryModel.Id != 0)
            {
                var findCategory = await appDbContext.Categories.FindAsync(categoryModel.Id);
                if (findCategory is null)
                    return (int)System.Net.HttpStatusCode.NotFound;

                if (findCategory.CountryName == categoryModel.CountryName && findCategory.Image == categoryModel.Image)
                    return (int)System.Net.HttpStatusCode.NotImplemented;

                findCategory.CountryName = categoryModel.CountryName;
                findCategory.Image = categoryModel.Image;
                await appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await appDbContext.Categories.Where(_ => _.CountryName.ToLower().Equals(categoryModel.CountryName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;

            appDbContext.Categories.Add(category);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }


        public async Task<int> DeleteCategoryAsync(int id)
        {
            Category category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            appDbContext.Categories.Remove(category);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            var categories = await appDbContext.Categories.ToListAsync();
            if (categories is null)
                return null!;

            var categoryModelList = categories.Select(mapper.Map<CategoryModel>);
            return categoryModelList.ToList();
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int id)
        {
            Category category = await appDbContext.Categories.FirstOrDefaultAsync(_ => _.Id == id);
            if (category is null) return null!;

            var categoryModel = mapper.Map<CategoryModel>(category);
            return categoryModel;
        }
    }
}
