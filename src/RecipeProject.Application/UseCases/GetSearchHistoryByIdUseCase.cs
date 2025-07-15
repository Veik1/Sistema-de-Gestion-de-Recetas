using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetSearchHistoryByIdUseCase
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public GetSearchHistoryByIdUseCase(ISearchHistoryRepository searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        public SearchHistory Execute(int searchHistoryId)
        {
            return _searchHistoryRepository.GetById(searchHistoryId);
        }
    }
}