using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListSearchHistoriesUseCase
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public ListSearchHistoriesUseCase(ISearchHistoryRepository searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        public IEnumerable<SearchHistory> Execute()
        {
            return _searchHistoryRepository.GetAll();
        }
    }
}