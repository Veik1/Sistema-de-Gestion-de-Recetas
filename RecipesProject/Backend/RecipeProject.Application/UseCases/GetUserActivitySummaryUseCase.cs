using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetUserActivitySummaryUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserActivitySummaryUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public object Execute(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return null;

            return new
            {
                user.Id,
                user.Name,
                CreatedRecipes = user.CreatedRecipes.Count,
                FavoriteRecipes = user.FavoriteRecipes.Count,
                Comments = user.Comments.Count,
                Ratings = user.Ratings.Count,
                SearchHistory = user.SearchHistories.Count
            };
        }
    }
}