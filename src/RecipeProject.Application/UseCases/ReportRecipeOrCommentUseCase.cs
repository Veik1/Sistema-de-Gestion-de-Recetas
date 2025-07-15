using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class ReportRecipeOrCommentUseCase
    {
        private readonly IReportRepository _reportRepository;

        public ReportRecipeOrCommentUseCase(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public void Execute(int userId, string reason, int? recipeId = null, int? commentId = null)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Reason is required.");

            var report = new Report
            {
                UserId = userId,
                RecipeId = recipeId,
                CommentId = commentId,
                Reason = reason,
                Date = DateTime.UtcNow,
                IsResolved = false
            };

            _reportRepository.Add(report);
        }
    }
}