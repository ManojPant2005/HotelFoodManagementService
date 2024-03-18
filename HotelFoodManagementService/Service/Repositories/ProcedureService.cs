using AutoMapper;
using HotelFoodManagementService.Components.Models;
using HotelFoodManagementService.Data;
using HotelFoodManagementService.Data.Entity;
using HotelFoodManagementService.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelFoodManagementService.Service.Repositories
{
    public class ProcedureService : IProcedureService
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        public ProcedureService(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task<int> AddOrUpdateStepAsync(StepModels stepModel)
        {
            if (stepModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var step = mapper.Map<Step>(stepModel);

            if (stepModel.Id != 0)
            {
                var findstep = await appDbContext.Procedures.FindAsync(stepModel.Id);
                if (findstep is null)
                    return (int)System.Net.HttpStatusCode.NotFound;

                findstep.ProcedureNo = stepModel.ProcedureNo;
                findstep.Description = stepModel.Description;
                findstep.Title = stepModel.Title;
                findstep.TimeNeeded = stepModel.TimeNeeded;
                findstep.RecipeId = stepModel.RecipeId;

                await appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await appDbContext.Procedures.Where(_ => _.Title.ToLower().Equals(stepModel.Title.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;

            appDbContext.Procedures.Add(step);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteStepAsync(int id)
        {
            Step step = await appDbContext.Procedures.FirstOrDefaultAsync(c => c.Id == id);
            if (step is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            appDbContext.Procedures.Remove(step);
            await appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }


        public async Task<StepModels> GetStepByIdAsync(int id)
        {
            Step step = await appDbContext.Procedures.FirstOrDefaultAsync(_ => _.Id == id);
            if (step is null) return null!;

            var stepModel = mapper.Map<StepModels>(step);
            return stepModel;
        }

        public async Task<List<StepModels>> GetStepByRecipeIdAsync(int recipeId)
        {
            var results = await appDbContext.Procedures.Where(_ => _.RecipeId == recipeId).ToListAsync();
            var list = results.Select(mapper.Map<StepModels>);
            return list.ToList();
        }

        public async Task<List<StepModels>> GetStepsAsync()
        {
            var steps = await appDbContext.Procedures.Include(_ => _.Recipe).ToListAsync();
            if (steps is null)
                return null!;

            var stepModelList = steps.Select(mapper.Map<StepModels>);
            return stepModelList.ToList();
        }
    }
}
