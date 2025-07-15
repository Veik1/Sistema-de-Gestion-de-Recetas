using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ListCommentsUseCase
    {
        private readonly ICommentRepository _commentRepository;

        public ListCommentsUseCase(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IEnumerable<Comment> Execute()
        {
            return _commentRepository.GetAll();
        }
    }
}