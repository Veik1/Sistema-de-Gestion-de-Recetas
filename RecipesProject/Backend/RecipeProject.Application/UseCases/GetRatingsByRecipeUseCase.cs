using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetRatingsByRecipeUseCase
    {
        private readonly IRatingRepository _ratingRepository;

        public GetRatingsByRecipeUseCase(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public IEnumerable<Rating> Execute(int recipeId)
        {
            return _ratingRepository.GetByRecipeId(recipeId);
        }
    }
}