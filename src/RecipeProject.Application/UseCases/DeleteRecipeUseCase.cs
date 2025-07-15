using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class DeleteRecipeUseCase
    {
        private readonly IRecipeRepository _recipeRepository;

        public DeleteRecipeUseCase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public void Execute(int recipeId)
        {
            var recipe = _recipeRepository.GetById(recipeId);
            if (recipe == null)
                throw new System.ArgumentException("Recipe does not exist.");

            _recipeRepository.Delete(recipeId);
        }
    }
}