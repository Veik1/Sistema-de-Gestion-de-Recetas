using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class UpdateSearchHistoryUseCase
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public UpdateSearchHistoryUseCase(ISearchHistoryRepository searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        public void Execute(SearchHistory updatedSearchHistory)
        {
            var existing = _searchHistoryRepository.GetById(updatedSearchHistory.Id);
            if (existing == null)
                throw new ArgumentException("Search history entry does not exist.");

            if (string.IsNullOrWhiteSpace(updatedSearchHistory.Query))
                throw new ArgumentException("Query is required.");

            existing.Query = updatedSearchHistory.Query;
            existing.Date = updatedSearchHistory.Date;

            _searchHistoryRepository.Update(existing);
        }
    }
}