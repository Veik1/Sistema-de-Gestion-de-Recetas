using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class CreateSearchHistoryUseCase
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;
        private readonly IUserRepository _userRepository;

        public CreateSearchHistoryUseCase(
            ISearchHistoryRepository searchHistoryRepository,
            IUserRepository userRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
            _userRepository = userRepository;
        }

        public void Execute(SearchHistory searchHistory)
        {
            if (string.IsNullOrWhiteSpace(searchHistory.Query))
                throw new ArgumentException("Query is required.");

            if (_userRepository.GetById(searchHistory.UserId) == null)
                throw new ArgumentException("User does not exist.");

            searchHistory.Date = DateTime.UtcNow;

            _searchHistoryRepository.Add(searchHistory);
        }
    }
}