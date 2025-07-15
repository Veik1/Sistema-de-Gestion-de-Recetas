using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecipesByUserRatingUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecipesByUserRatingUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(int userId, int minRating = 1)
        {
            return _recipeRepository.GetAll()
                .Where(r => r.Ratings.Any(rt => rt.UserId == userId && rt.Score >= minRating));
        }
    }
}