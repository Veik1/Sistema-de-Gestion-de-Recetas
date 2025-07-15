using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class DeleteCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Execute(int categoryId)
        {
            var category = _categoryRepository.GetById(categoryId);
            if (category == null)
                throw new System.ArgumentException("Category does not exist.");

            _categoryRepository.Delete(categoryId);
        }
    }
}