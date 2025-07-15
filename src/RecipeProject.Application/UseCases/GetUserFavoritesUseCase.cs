using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetUserFavoritesUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserFavoritesUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<Recipe> Execute(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new System.ArgumentException("User does not exist.");

            return user.FavoriteRecipes ?? new List<Recipe>();
        }
    }
}