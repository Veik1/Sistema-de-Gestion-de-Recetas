using System;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetUserMostActiveDayUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserMostActiveDayUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public DateTime? Execute(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return null;

            var allDates = user.CreatedRecipes.Select(r => r.CreationDate)
                .Concat(user.Comments.Select(c => c.Date))
                .Concat(user.Ratings.Select(r => r.Recipe != null ? r.Recipe.CreationDate : DateTime.MinValue))
                .Concat(user.SearchHistories.Select(s => s.Date))
                .Where(d => d != DateTime.MinValue)
                .ToList();

            if (!allDates.Any())
                return null;

            return allDates
                .GroupBy(d => d.Date)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }
    }
}