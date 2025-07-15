using System;
using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecentlyCreatedRecipesUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecentlyCreatedRecipesUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(int days = 7)
        {
            var since = DateTime.UtcNow.AddDays(-days);
            return _recipeRepository.GetAll()
                .Where(r => r.CreationDate >= since);
        }
    }
}