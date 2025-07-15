using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetSearchHistoryByUserUseCase
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public GetSearchHistoryByUserUseCase(ISearchHistoryRepository searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        public IEnumerable<SearchHistory> Execute(int userId)
        {
            return _searchHistoryRepository.GetByUserId(userId);
        }
    }
}