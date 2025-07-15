using System;

namespace RecipeProject.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int? RecipeId { get; set; }
        public int? CommentId { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public bool IsResolved { get; set; }
    }
}