using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetCommentsByRecipeUseCase
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsByRecipeUseCase(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IEnumerable<Comment> Execute(int recipeId)
        {
            return _commentRepository.GetByRecipeId(recipeId);
        }
    }
}