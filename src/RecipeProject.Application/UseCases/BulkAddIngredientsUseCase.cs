using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class BulkAddIngredientsUseCase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public BulkAddIngredientsUseCase(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public void Execute(IEnumerable<Ingredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                if (!string.IsNullOrWhiteSpace(ingredient.Name) && !string.IsNullOrWhiteSpace(ingredient.Quantity))
                {
                    _ingredientRepository.Add(ingredient);
                }
            }
        }
    }
}