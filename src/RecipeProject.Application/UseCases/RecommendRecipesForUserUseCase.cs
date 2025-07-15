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

            var favoriteCategoryIds = user.FavoriteRecipes
                .SelectMany(r => r.Categories)
                .GroupBy(c => c.Id)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(3)
                .ToList();

            var favoriteRecipeIds = user.FavoriteRecipes.Select(r => r.Id).ToHashSet();

            return _recipeRepository.GetAll()
                .Where(r => r.Categories.Any(c => favoriteCategoryIds.Contains(c.Id)) &&
                            !favoriteRecipeIds.Contains(r.Id))
                .Take(topN)
                .ToList();
        }
    }
}