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
            // Obtener todos los usuarios y sus recetas favoritas
            var allUsers = _userRepository.GetAll();

            // Contar cuántas veces cada receta aparece en favoritos
            var recipeFavoriteCounts = allUsers
                .SelectMany(u => u.FavoriteRecipes)
                .GroupBy(r => r.Id)
                .Select(g => new { RecipeId = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(topN)
                .ToList();

            // Obtener las recetas completas a partir de los IDs
            var recipes = _recipeRepository.GetAll().ToList();
            var mostFavoritedRecipes = recipeFavoriteCounts
                .Select(x => recipes.FirstOrDefault(r => r.Id == x.RecipeId))
                .Where(r => r != null)
                .ToList();

            return mostFavoritedRecipes;
        }
    }
}