using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListIngredientsUseCase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public ListIngredientsUseCase(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<Ingredient> Execute()
        {
            return _ingredientRepository.GetAll();
        }
    }
}