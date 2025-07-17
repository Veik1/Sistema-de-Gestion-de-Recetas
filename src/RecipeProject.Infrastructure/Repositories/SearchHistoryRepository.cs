using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using RecipeProject.Infrastructure.Data;

namespace RecipeProject.Infrastructure.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly AppDbContext _context;

        public SearchHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SearchHistory> GetAll() =>
            _context.SearchHistories.ToList();

        public SearchHistory? GetById(int id) =>
            _context.SearchHistories.FirstOrDefault(sh => sh.Id == id);

        public IEnumerable<SearchHistory> GetByUserId(int userId) =>
            _context.SearchHistories.Where(sh => sh.UserId == userId).ToList();

        public void Add(SearchHistory history)
        {
            _context.SearchHistories.Add(history);
            _context.SaveChanges();
        }

        public void Update(SearchHistory history)
        {
            _context.SearchHistories.Update(history);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var history = _context.SearchHistories.FirstOrDefault(sh => sh.Id == id);
            if (history != null)
            {
                _context.SearchHistories.Remove(history);
                _context.SaveChanges();
            }
        }
    }
}