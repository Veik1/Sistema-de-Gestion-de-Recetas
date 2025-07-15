using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecipesWithNoRatingsUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecipesWithNoRatingsUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute()
        {
            return _recipeRepository.GetAll()
                .Where(r => r.Ratings == null || r.Ratings.Count == 0);
        }
    }
}