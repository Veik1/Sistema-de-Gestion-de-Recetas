using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecipesUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecipesUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute()
        {
            return _recipeRepository.GetAll();
        }
    }
}