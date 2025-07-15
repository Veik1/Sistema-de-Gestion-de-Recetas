using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecipesByExactIngredientCountUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecipesByExactIngredientCountUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(int ingredientCount)
        {
            return _recipeRepository.GetAll()
                .Where(r => r.Ingredients.Count == ingredientCount);
        }
    }
}