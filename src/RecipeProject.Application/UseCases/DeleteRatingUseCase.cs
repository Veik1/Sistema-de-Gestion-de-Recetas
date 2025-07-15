using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class DeleteRatingUseCase
    {
        private readonly IRatingRepository _ratingRepository;

        public DeleteRatingUseCase(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public void Execute(int ratingId)
        {
            var rating = _ratingRepository.GetById(ratingId);
            if (rating == null)
                throw new System.ArgumentException("Rating does not exist.");

            _ratingRepository.Delete(ratingId);
        }
    }
}