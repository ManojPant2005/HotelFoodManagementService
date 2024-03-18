using AutoMapper;
using HotelFoodManagementService.Components.Models;
using HotelFoodManagementService.Data;
using HotelFoodManagementService.Data.Entity;
using HotelFoodManagementService.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelFoodManagementService.Service.Repositories
{
    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        public RecipeService(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task<int> AddOrUpdateRecipeAsync(RecipeModel recipeModel)
        {
            if (recipeModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var recipe = mapper.Map<Recipe>(recipeModel);

            if (recipeModel.Id != 0)
            {
                var findRecipe = await appDbContext.Recipes.FindAsync(recipeModel.Id);
                if (findRecipe is null)
                    return (int)System.Net.HttpStatusCode.NotFound;

                findRecipe.RecipeName = recipeModel.RecipeName;
                findRecipe.Rank = recipeModel.Rank;
                findRecipe.GeneralTimeNeeded = recipeModel.GeneralTimeNeeded;
                findRecipe.CategoryId = recipeModel.CategoryId;
                findRecipe.Description = recipeModel.Description;
                findRecipe.GeneralImage = recipeModel.GeneralImage;

                await appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await appDbContext.Recipes.Where(_ => _.RecipeName.ToLower().Equals(recipeModel.RecipeName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;

            appDbContext.Recipes.Add(recipe);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteRecipeAsync(int id)
        {
            Recipe recipe = await appDbContext.Recipes.FirstOrDefaultAsync(c => c.Id == id);
            if (recipe is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            appDbContext.Recipes.Remove(recipe);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<RecipeModel>> GetRecipeByCategoryIdAsync(int categoryId)
        {
            var results = await appDbContext.Recipes.Where(_ => _.CategoryId == categoryId).Include(_ => _.Category).ToListAsync();
            var list = results.Select(mapper.Map<RecipeModel>);
            return list.ToList();
        }
        public async Task<List<RecipeModel>> GetRecipesAsync()
        {
            var recipes = await appDbContext.Recipes.ToListAsync();
            if (recipes is null)
                return null!;

            var recipeModelList = recipes.Select(mapper.Map<RecipeModel>);
            return recipeModelList.ToList();
        }
        public async Task<RecipeModel> GetRecipeByIdAsync(int id)
        {
            Recipe recipe = await appDbContext.Recipes.Include(_ => _.Category).Include(_ => _.Procedures).FirstOrDefaultAsync(_ => _.Id == id);
            if (recipe is null) return null!;

            var recipeModel = mapper.Map<RecipeModel>(recipe);
            return recipeModel;
        }
    }
}

