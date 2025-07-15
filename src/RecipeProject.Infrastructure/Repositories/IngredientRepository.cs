using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using RecipeProject.Infrastructure.Data;

namespace RecipeProject.Infrastructure.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly AppDbContext _context;

        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ingredient> GetAll() =>
            _context.Ingredients.Include(i => i.Recipe).ToList();

        public Ingredient GetById(int id) =>
            _context.Ingredients.Include(i => i.Recipe).FirstOrDefault(i => i.Id == id);

        public IEnumerable<Ingredient> GetByRecipeId(int recipeId) =>
            _context.Ingredients.Where(i => i.RecipeId == recipeId).ToList();

        public void Add(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
        }

        public void Update(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(i => i.Id == id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                _context.SaveChanges();
            }
        }
    }
}