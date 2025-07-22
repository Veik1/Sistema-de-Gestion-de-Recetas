using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using RecipeProject.Infrastructure.Data;

namespace RecipeProject.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Recipe> GetAll() =>
            _context.Recipes
                .Include(r => r.Categories)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .ToList();

        public Recipe GetById(int id) =>
            _context.Recipes
                .Include(r => r.Categories)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .FirstOrDefault(r => r.Id == id);

        public IEnumerable<Recipe> GetByUserId(int userId) =>
            _context.Recipes
                .Where(r => r.UserId == userId)
                .Include(r => r.Categories)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .ToList();

        public void Add(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }

        public void Update(Recipe recipe)
        {
            // Adjunta la receta
            _context.Recipes.Update(recipe);

            // Adjunta ingredientes existentes
            foreach (var ri in recipe.RecipeIngredients)
            {
                if (ri.IngredientId > 0)
                    _context.Entry(ri.Ingredient).State = EntityState.Unchanged;
            }

            // Adjunta categorías existentes
            foreach (var cat in recipe.Categories)
            {
                if (cat.Id > 0)
                    _context.Entry(cat).State = EntityState.Unchanged;
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }
        }
    }
}