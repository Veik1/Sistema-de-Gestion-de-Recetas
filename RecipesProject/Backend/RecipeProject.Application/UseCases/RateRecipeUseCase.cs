using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class RateRecipeUseCase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;

        public RateRecipeUseCase(
            IRatingRepository ratingRepository,
            IUserRepository userRepository,
            IRecipeRepository recipeRepository)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public void Execute(Rating rating)
        {
            // Business Rule: Score must be between 1 and 5
            if (rating.Score < 1 || rating.Score > 5)
                throw new ArgumentException("Score must be between 1 and 5.");

            // Business Rule: User must exist
            if (_userRepository.GetById(rating.UserId) == null)
                throw new ArgumentException("User does not exist.");

            // Business Rule: Recipe must exist
            if (_recipeRepository.GetById(rating.RecipeId) == null)
                throw new ArgumentException("Recipe does not exist.");

            // Business Rule: User can rate a recipe only once (optional, if needed)
            foreach (var existingRating in _ratingRepository.GetByRecipeId(rating.RecipeId))
            {
                if (existingRating.UserId == rating.UserId)
                    throw new InvalidOperationException("User has already rated this recipe.");
            }

            _ratingRepository.Add(rating);
        }
    }
}