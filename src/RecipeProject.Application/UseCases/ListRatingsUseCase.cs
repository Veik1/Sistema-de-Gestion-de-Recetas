using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRatingsUseCase
    {
        private readonly IRatingRepository _ratingRepository;

        public ListRatingsUseCase(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public IEnumerable<Rating> Execute()
        {
            return _ratingRepository.GetAll();
        }
    }
}