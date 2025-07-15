using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecipesByCategoryUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecipesByCategoryUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return new List<Recipe>();

            return _recipeRepository.GetAll()
                .Where(r => r.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower()));
        }
    }
}