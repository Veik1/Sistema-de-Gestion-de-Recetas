using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class UpdateRatingUseCase
    {
        private readonly IRatingRepository _ratingRepository;

        public UpdateRatingUseCase(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public void Execute(Rating updatedRating)
        {
            var existingRating = _ratingRepository.GetById(updatedRating.Id);
            if (existingRating == null)
                throw new ArgumentException("Rating does not exist.");

            if (updatedRating.Score < 1 || updatedRating.Score > 5)
                throw new ArgumentException("Score must be between 1 and 5.");

            existingRating.Score = updatedRating.Score;

            _ratingRepository.Update(existingRating);
        }
    }
}