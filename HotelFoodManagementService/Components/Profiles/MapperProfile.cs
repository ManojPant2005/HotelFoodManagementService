using AutoMapper;
using HotelFoodManagementService.Components.Models;
using HotelFoodManagementService.Data.Entity;

namespace HotelFoodManagementService.Components.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //from database - user
            CreateMap<Category, CategoryModel>();
            CreateMap<Recipe, RecipeModel>();
            CreateMap<Step, StepModels>();

            //from user - database
            CreateMap<CategoryModel, Category>();
            CreateMap<RecipeModel, Recipe>();
            CreateMap<StepModels, Step>();


        }
    }
}
