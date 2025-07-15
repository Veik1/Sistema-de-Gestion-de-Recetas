using System;
using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListInactiveUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public ListInactiveUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> Execute(int daysWithoutActivity = 30)
        {
            var since = DateTime.UtcNow.AddDays(-daysWithoutActivity);
            return _userRepository.GetAll()
                .Where(u =>
                    (u.CreatedRecipes == null || !u.CreatedRecipes.Any(r => r.CreationDate >= since)) &&
                    (u.Comments == null || !u.Comments.Any(c => c.Date >= since)) &&
                    (u.Ratings == null || !u.Ratings.Any(r => r.Recipe != null && r.Recipe.CreationDate >= since)) &&
                    (u.SearchHistories == null || !u.SearchHistories.Any(s => s.Date >= since))
                );
        }
    }
}