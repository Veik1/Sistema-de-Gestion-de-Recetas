using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class UpdateIngredientUseCase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public UpdateIngredientUseCase(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public void Execute(Ingredient updatedIngredient)
        {
            var existingIngredient = _ingredientRepository.GetById(updatedIngredient.Id);
            if (existingIngredient == null)
                throw new ArgumentException("Ingredient does not exist.");

            if (string.IsNullOrWhiteSpace(updatedIngredient.Name))
                throw new ArgumentException("Ingredient name is required.");
            if (string.IsNullOrWhiteSpace(updatedIngredient.Quantity))
                throw new ArgumentException("Ingredient quantity is required.");

            existingIngredient.Name = updatedIngredient.Name;
            existingIngredient.Quantity = updatedIngredient.Quantity;

            _ingredientRepository.Update(existingIngredient);
        }
    }
}