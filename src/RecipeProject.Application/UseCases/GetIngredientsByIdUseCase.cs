using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetIngredientByIdUseCase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public GetIngredientByIdUseCase(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public Ingredient Execute(int ingredientId)
        {
            return _ingredientRepository.GetById(ingredientId);
        }
    }
}