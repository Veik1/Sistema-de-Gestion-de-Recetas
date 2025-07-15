using System.Linq;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetAverageRatingForRecipeUseCase
    {
        private readonly IRatingRepository _ratingRepository;

        public GetAverageRatingForRecipeUseCase(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public double Execute(int recipeId)
        {
            var ratings = _ratingRepository.GetByRecipeId(recipeId).ToList();
            if (ratings.Count == 0)
                return 0.0;
            return ratings.Average(r => r.Score);
        }
    }
}