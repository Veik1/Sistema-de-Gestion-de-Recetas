using System.Collections.Generic;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class BulkDeleteRecipesUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public BulkDeleteRecipesUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public void Execute(IEnumerable<int> recipeIds)
        {
            foreach (var id in recipeIds)
            {
                var recipe = _recipeRepository.GetById(id);
                if (recipe != null)
                {
                    _recipeRepository.Delete(id);
                }
            }
        }
    }
}