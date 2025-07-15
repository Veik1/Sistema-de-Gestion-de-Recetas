using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecipesByMultipleCategoriesUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecipesByMultipleCategoriesUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(IEnumerable<int> categoryIds)
        {
            var categoryIdSet = new HashSet<int>(categoryIds);
            return _recipeRepository.GetAll()
                .Where(r => r.Categories.Any(c => categoryIdSet.Contains(c.Id)));
        }
    }
}