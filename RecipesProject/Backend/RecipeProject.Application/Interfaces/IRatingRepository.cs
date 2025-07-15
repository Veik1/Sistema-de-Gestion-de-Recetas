using System.Collections.Generic;
using RecipeProject.Domain.Entities;

namespace RecipeProject.Application.Interfaces
{
    public interface IRatingRepository
    {
        Rating GetById(int id);
        IEnumerable<Rating> GetAll();
        IEnumerable<Rating> GetByUserId(int userId);
        IEnumerable<Rating> GetByRecipeId(int recipeId);
        void Add(Rating rating);
        void Update(Rating rating);
        void Delete(int id);
    }
}