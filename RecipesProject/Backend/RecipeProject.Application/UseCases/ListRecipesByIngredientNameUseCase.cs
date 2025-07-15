using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecipesByIngredientNameUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecipesByIngredientNameUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(string ingredientName)
        {
            if (string.IsNullOrWhiteSpace(ingredientName))
                return new List<Recipe>();

            return _recipeRepository.GetAll()
                .Where(r => r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientName.ToLower())));
        }
    }
}