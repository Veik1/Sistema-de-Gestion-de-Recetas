using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListRecipesPagedUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public ListRecipesPagedUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> Execute(int page = 1, int pageSize = 10, string orderBy = "date", bool descending = true)
        {
            var query = _recipeRepository.GetAll().AsQueryable();

            switch (orderBy.ToLower())
            {
                case "title":
                    query = descending ? query.OrderByDescending(r => r.Title) : query.OrderBy(r => r.Title);
                    break;
                case "date":
                default:
                    query = descending ? query.OrderByDescending(r => r.CreationDate) : query.OrderBy(r => r.CreationDate);
                    break;
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}