using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class CreateIngredientUseCase
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;

        public CreateIngredientUseCase(
            IIngredientRepository ingredientRepository,
            IRecipeRepository recipeRepository)
        {
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
        }

        public void Execute(Ingredient ingredient)
        {
            if (string.IsNullOrWhiteSpace(ingredient.Name))
                throw new ArgumentException("Ingredient name is required.");

            if (string.IsNullOrWhiteSpace(ingredient.Quantity))
                throw new ArgumentException("Ingredient quantity is required.");

            if (_recipeRepository.GetById(ingredient.RecipeId) == null)
                throw new ArgumentException("Recipe does not exist.");

            _ingredientRepository.Add(ingredient);
        }
    }
}