using System.Collections.Generic;
using RecipeProject.Domain.Entities;

namespace RecipeProject.Application.Interfaces
{
    public interface IRecipeRepository
    {
        Recipe GetById(int id);
        IEnumerable<Recipe> GetAll();
        IEnumerable<Recipe> GetByUserId(int userId);
        void Add(Recipe recipe);
        void Update(Recipe recipe);
        void Delete(int id);
    }
}