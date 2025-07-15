using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class RecommendRecipesForUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;

        public RecommendRecipesForUserUseCase(IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(int userId, int topN = 5)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return new List<Recipe>();

            // Encuentra las categorías más usadas por el usuario
            var favoriteCategories = user.FavoriteRecipes
                .SelectMany(r => r.Categories)
                .GroupBy(c => c.Id)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(3)
                .ToList();

            // Recomienda recetas de esas categorías que el usuario no haya marcado como favoritas
            return _recipeRepository.GetAll()
                .Where(r => r.Categories.Any(c => favoriteCategories.Contains(c.Id)) &&
                            !user.FavoriteRecipes.Any(fr => fr.Id == r.Id))
                .Take(topN)
                .ToList();
        }
    }
}