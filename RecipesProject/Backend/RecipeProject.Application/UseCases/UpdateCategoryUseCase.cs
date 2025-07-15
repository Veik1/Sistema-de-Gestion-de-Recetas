using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class UpdateCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Execute(Category updatedCategory)
        {
            var existingCategory = _categoryRepository.GetById(updatedCategory.Id);
            if (existingCategory == null)
                throw new ArgumentException("Category does not exist.");

            if (string.IsNullOrWhiteSpace(updatedCategory.Name))
                throw new ArgumentException("Category name is required.");

            existingCategory.Name = updatedCategory.Name;
            existingCategory.Icon = updatedCategory.Icon;

            _categoryRepository.Update(existingCategory);
        }
    }
}