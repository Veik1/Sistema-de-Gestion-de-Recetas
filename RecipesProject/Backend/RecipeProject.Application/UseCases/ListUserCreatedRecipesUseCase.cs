using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListUserCreatedRecipesUseCase
    {
        private readonly IUserRepository _userRepository;

        public ListUserCreatedRecipesUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<Recipe> Execute(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return new List<Recipe>();

            return user.CreatedRecipes ?? new List<Recipe>();
        }
    }
}