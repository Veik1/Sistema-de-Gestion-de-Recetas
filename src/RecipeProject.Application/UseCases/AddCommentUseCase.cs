using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class AddCommentUseCase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;

        public AddCommentUseCase(
            ICommentRepository commentRepository,
            IUserRepository userRepository,
            IRecipeRepository recipeRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public void Execute(Comment comment)
        {
            // Business Rule: Text is required
            if (string.IsNullOrWhiteSpace(comment.Text))
                throw new ArgumentException("Comment text is required.");

            // Business Rule: User must exist
            if (_userRepository.GetById(comment.UserId) == null)
                throw new ArgumentException("User does not exist.");

            // Business Rule: Recipe must exist
            if (_recipeRepository.GetById(comment.RecipeId) == null)
                throw new ArgumentException("Recipe does not exist.");

            // Set comment date
            comment.Date = DateTime.UtcNow;

            _commentRepository.Add(comment);
        }
    }
}