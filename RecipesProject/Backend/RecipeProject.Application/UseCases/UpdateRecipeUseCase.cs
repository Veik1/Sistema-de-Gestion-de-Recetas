using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class UpdateRecipeUseCase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;

        public UpdateRecipeUseCase(IRecipeRepository recipeRepository, IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
        }

        public void Execute(Recipe updatedRecipe)
        {
            var existingRecipe = _recipeRepository.GetById(updatedRecipe.Id);
            if (existingRecipe == null)
                throw new ArgumentException("Recipe does not exist.");

            // Optional: Only the creator can update the recipe
            var user = _userRepository.GetById(updatedRecipe.UserId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            // Business rules: Title and Instructions required
            if (string.IsNullOrWhiteSpace(updatedRecipe.Title))
                throw new ArgumentException("Title is required.");
            if (string.IsNullOrWhiteSpace(updatedRecipe.Instructions))
                throw new ArgumentException("Instructions are required.");

            // Update fields
            existingRecipe.Title = updatedRecipe.Title;
            existingRecipe.Instructions = updatedRecipe.Instructions;
            existingRecipe.ImageUrl = updatedRecipe.ImageUrl;
            existingRecipe.Categories = updatedRecipe.Categories;
            existingRecipe.Ingredients = updatedRecipe.Ingredients;
            existingRecipe.IsGeneratedByAI = updatedRecipe.IsGeneratedByAI;

            _recipeRepository.Update(existingRecipe);
        }
    }
}