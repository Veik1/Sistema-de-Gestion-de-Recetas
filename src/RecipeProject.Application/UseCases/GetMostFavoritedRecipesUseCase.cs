using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetMostFavoritedRecipesUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;

        public GetMostFavoritedRecipesUseCase(IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(int topN = 10)
        {
            // Obtener todos los IDs de recetas favoritas
            var favoriteRecipeIds = _userRepository.GetAll()
                .SelectMany(u => u.FavoriteRecipes.Select(r => r.Id))
                .GroupBy(id => id)
                .OrderByDescending(g => g.Count())
                .Take(topN)
                .Select(g => g.Key)
                .ToList();

            // Obtener solo las recetas necesarias
            return _recipeRepository.GetAll()
                .Where(r => favoriteRecipeIds.Contains(r.Id))
                .ToList();
        }
    }
}