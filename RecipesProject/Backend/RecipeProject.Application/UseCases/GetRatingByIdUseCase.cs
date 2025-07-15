using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetRatingByIdUseCase
    {
        private readonly IRatingRepository _ratingRepository;

        public GetRatingByIdUseCase(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public Rating Execute(int ratingId)
        {
            return _ratingRepository.GetById(ratingId);
        }
    }
}