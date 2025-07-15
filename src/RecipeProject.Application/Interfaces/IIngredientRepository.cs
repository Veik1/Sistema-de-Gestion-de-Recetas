using System.Collections.Generic;
using RecipeProject.Domain.Entities;

namespace RecipeProject.Application.Interfaces
{
    public interface IIngredientRepository
    {
        Ingredient GetById(int id);
        IEnumerable<Ingredient> GetAll();
        IEnumerable<Ingredient> GetByRecipeId(int recipeId);
        void Add(Ingredient ingredient);
        void Update(Ingredient ingredient);
        void Delete(int id);
    }
}