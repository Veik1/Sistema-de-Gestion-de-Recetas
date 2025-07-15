using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetTopRatedRecipesUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetTopRatedRecipesUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(int topN = 10)
        {
            return _recipeRepository.GetAll()
                .OrderByDescending(r => r.Ratings.Any() ? r.Ratings.Average(rt => rt.Score) : 0)
                .ThenByDescending(r => r.Ratings.Count)
                .Take(topN);
        }
    }
}