using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using RecipeProject.Infrastructure.Data;

namespace RecipeProject.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAll() =>
            _context.Comments.Include(c => c.User).Include(c => c.Recipe).ToList();

        public Comment GetById(int id) =>
            _context.Comments.Include(c => c.User).Include(c => c.Recipe).FirstOrDefault(c => c.Id == id);

        public IEnumerable<Comment> GetByUserId(int userId) =>
            _context.Comments.Where(c => c.UserId == userId).Include(c => c.Recipe).ToList();

        public IEnumerable<Comment> GetByRecipeId(int recipeId) =>
            _context.Comments.Where(c => c.RecipeId == recipeId).Include(c => c.User).ToList();

        public void Add(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void Update(Comment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }
    }
}