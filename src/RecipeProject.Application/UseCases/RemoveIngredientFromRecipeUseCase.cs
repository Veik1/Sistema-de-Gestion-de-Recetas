using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class RemoveIngredientFromRecipeUseCase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public RemoveIngredientFromRecipeUseCase(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public void Execute(int ingredientId)
        {
            var ingredient = _ingredientRepository.GetById(ingredientId);
            if (ingredient == null)
                throw new System.ArgumentException("Ingredient does not exist.");

            _ingredientRepository.Delete(ingredientId);
        }
    }
}