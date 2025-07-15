using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class GetCommentByIdUseCase
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentByIdUseCase(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Comment Execute(int commentId)
        {
            return _commentRepository.GetById(commentId);
        }
    }
}