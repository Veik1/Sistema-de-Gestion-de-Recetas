using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class SearchRecipesUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public SearchRecipesUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<Recipe>();

            return _recipeRepository.GetAll()
                .Where(r =>
                    (!string.IsNullOrEmpty(r.Title) && r.Title.ToLower().Contains(query.ToLower())) ||
                    (!string.IsNullOrEmpty(r.Instructions) && r.Instructions.ToLower().Contains(query.ToLower()))
                );
        }
    }
}