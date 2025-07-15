using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetMostCommentedRecipesUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetMostCommentedRecipesUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(int topN = 10)
        {
            return _recipeRepository.GetAll()
                .OrderByDescending(r => r.Comments.Count)
                .Take(topN);
        }
    }
}