using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class DeleteSearchHistoryUseCase
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public DeleteSearchHistoryUseCase(ISearchHistoryRepository searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        public void Execute(int searchHistoryId)
        {
            var entry = _searchHistoryRepository.GetById(searchHistoryId);
            if (entry == null)
                throw new System.ArgumentException("Search history entry does not exist.");

            _searchHistoryRepository.Delete(searchHistoryId);
        }
    }
}