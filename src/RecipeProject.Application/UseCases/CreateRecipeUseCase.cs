using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class CreateRecipeUseCase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;

        public CreateRecipeUseCase(IRecipeRepository recipeRepository, IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
        }

        public void Execute(Recipe recipe)
        {

            if (string.IsNullOrWhiteSpace(recipe.Title))
                throw new ArgumentException("Title is required.");
            if (string.IsNullOrWhiteSpace(recipe.Instructions))
                throw new ArgumentException("Instructions are required.");


            if (recipe.Ingredients == null || recipe.Ingredients.Count == 0)
                throw new ArgumentException("At least one ingredient is required.");

            var user = _userRepository.GetById(recipe.UserId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            recipe.CreationDate = DateTime.UtcNow;

            _recipeRepository.Add(recipe);
        }
    }
}