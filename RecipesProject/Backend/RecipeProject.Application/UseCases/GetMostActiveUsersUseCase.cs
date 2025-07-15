using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetMostActiveUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetMostActiveUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> Execute(int topN = 10)
        {
            return _userRepository.GetAll()
                .OrderByDescending(u => u.CreatedRecipes.Count + u.Comments.Count + u.Ratings.Count)
                .Take(topN);
        }
    }
}