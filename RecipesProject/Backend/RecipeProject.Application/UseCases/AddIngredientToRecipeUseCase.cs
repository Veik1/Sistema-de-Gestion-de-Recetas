using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class AddIngredientToRecipeUseCase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public AddIngredientToRecipeUseCase(IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
        }

        public void Execute(int recipeId, Ingredient ingredient)
        {
            var recipe = _recipeRepository.GetById(recipeId);
            if (recipe == null)
                throw new ArgumentException("Recipe does not exist.");

            if (string.IsNullOrWhiteSpace(ingredient.Name) || string.IsNullOrWhiteSpace(ingredient.Quantity))
                throw new ArgumentException("Ingredient name and quantity are required.");

            ingredient.RecipeId = recipeId;
            _ingredientRepository.Add(ingredient);
        }
    }
}