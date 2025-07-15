using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecipesWithLowAverageRatingUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecipesWithLowAverageRatingUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(double maxAverageRating = 2.0)
        {
            return _recipeRepository.GetAll()
                .Where(r => r.Ratings.Any() && r.Ratings.Average(rt => rt.Score) <= maxAverageRating);
        }
    }
}