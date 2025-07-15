using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class CreateCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Execute(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name is required.");

            if (_categoryRepository.GetByName(category.Name) != null)
                throw new InvalidOperationException("Category name already exists.");

            _categoryRepository.Add(category);
        }
    }
}