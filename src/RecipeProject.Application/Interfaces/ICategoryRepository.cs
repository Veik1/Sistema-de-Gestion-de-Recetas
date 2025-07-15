using System.Collections.Generic;
using RecipeProject.Domain.Entities;

namespace RecipeProject.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetById(int id);
        IEnumerable<Category> GetAll();
        Category GetByName(string name);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}