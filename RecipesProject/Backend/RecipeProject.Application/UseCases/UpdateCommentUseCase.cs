using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class UpdateCommentUseCase
    {
        private readonly ICommentRepository _commentRepository;

        public UpdateCommentUseCase(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void Execute(Comment updatedComment)
        {
            var existingComment = _commentRepository.GetById(updatedComment.Id);
            if (existingComment == null)
                throw new ArgumentException("Comment does not exist.");

            if (string.IsNullOrWhiteSpace(updatedComment.Text))
                throw new ArgumentException("Comment text is required.");

            existingComment.Text = updatedComment.Text;
            existingComment.Date = DateTime.UtcNow;

            _commentRepository.Update(existingComment);
        }
    }
}