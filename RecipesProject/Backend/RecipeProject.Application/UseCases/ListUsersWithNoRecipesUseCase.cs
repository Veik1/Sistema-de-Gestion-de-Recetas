using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListUsersWithNoRecipesUseCase
    {
        private readonly IUserRepository _userRepository;

        public ListUsersWithNoRecipesUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> Execute()
        {
            return _userRepository.GetAll()
                .Where(u => u.CreatedRecipes == null || u.CreatedRecipes.Count == 0);
        }
    }
}