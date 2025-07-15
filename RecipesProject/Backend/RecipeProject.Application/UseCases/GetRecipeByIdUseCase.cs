using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetRecipeByIdUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetRecipeByIdUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Recipe Execute(int recipeId)
        {
            return _recipeRepository.GetById(recipeId);
        }
    }
}