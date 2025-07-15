using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class DeleteCommentUseCase
    {
        private readonly ICommentRepository _commentRepository;

        public DeleteCommentUseCase(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void Execute(int commentId)
        {
            var comment = _commentRepository.GetById(commentId);
            if (comment == null)
                throw new System.ArgumentException("Comment does not exist.");

            _commentRepository.Delete(commentId);
        }
    }
}