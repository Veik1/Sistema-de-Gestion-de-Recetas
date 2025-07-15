using System.Collections.Generic;
using RecipeProject.Domain.Entities;

namespace RecipeProject.Application.Interfaces
{
    public interface ISearchHistoryRepository
    {
        SearchHistory GetById(int id);
        IEnumerable<SearchHistory> GetAll();
        IEnumerable<SearchHistory> GetByUserId(int userId);
        void Add(SearchHistory searchHistory);
        void Update(SearchHistory searchHistory);
        void Delete(int id);
    }
}