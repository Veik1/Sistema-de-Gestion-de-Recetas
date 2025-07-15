using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class AddRecipeToFavoritesUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;

        public AddRecipeToFavoritesUseCase(IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public void Execute(int userId, int recipeId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            var recipe = _recipeRepository.GetById(recipeId);
            if (recipe == null)
                throw new ArgumentException("Recipe does not exist.");

            if (user.FavoriteRecipes.Exists(r => r.Id == recipeId))
                throw new InvalidOperationException("Recipe is already in favorites.");

            user.FavoriteRecipes.Add(recipe);
            _userRepository.Update(user);
        }
    }
}