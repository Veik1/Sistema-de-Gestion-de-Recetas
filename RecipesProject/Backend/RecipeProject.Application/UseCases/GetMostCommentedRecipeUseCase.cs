using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetUserMostCommentedRecipeUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserMostCommentedRecipeUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Recipe Execute(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null || user.CreatedRecipes == null || user.CreatedRecipes.Count == 0)
                return null;

            return user.CreatedRecipes
                .OrderByDescending(r => r.Comments?.Count ?? 0)
                .FirstOrDefault();
        }
    }
}