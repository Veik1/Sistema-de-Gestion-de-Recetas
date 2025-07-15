using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using RecipeProject.Infrastructure.Data;

namespace RecipeProject.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Rating> GetAll() =>
            _context.Ratings.Include(r => r.User).Include(r => r.Recipe).ToList();

        public Rating GetById(int id) =>
            _context.Ratings.Include(r => r.User).Include(r => r.Recipe).FirstOrDefault(r => r.Id == id);

        public IEnumerable<Rating> GetByRecipeId(int recipeId) =>
            _context.Ratings.Where(r => r.RecipeId == recipeId).Include(r => r.User).ToList();

        public IEnumerable<Rating> GetByUserId(int userId) =>
            _context.Ratings.Where(r => r.UserId == userId).Include(r => r.Recipe).ToList();

        public void Add(Rating rating)
        {
            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }

        public void Update(Rating rating)
        {
            _context.Ratings.Update(rating);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var rating = _context.Ratings.FirstOrDefault(r => r.Id == id);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                _context.SaveChanges();
            }
        }
    }
}