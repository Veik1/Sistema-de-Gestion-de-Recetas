using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using System;

namespace RecipeProject.Application.UseCases
{
    public class RemoveRecipeFromFavoritesUseCase
    {
        private readonly IUserRepository _userRepository;

        public RemoveRecipeFromFavoritesUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(int userId, int recipeId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            var recipe = user.FavoriteRecipes.Find(r => r.Id == recipeId);
            if (recipe == null)
                throw new ArgumentException("Recipe is not in favorites.");

            user.FavoriteRecipes.Remove(recipe);
            _userRepository.Update(user);
        }
    }
}