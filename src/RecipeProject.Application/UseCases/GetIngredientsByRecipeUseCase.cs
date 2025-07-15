using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetIngredientsByRecipeUseCase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public GetIngredientsByRecipeUseCase(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<Ingredient> Execute(int recipeId)
        {
            return _ingredientRepository.GetByRecipeId(recipeId);
        }
    }
}