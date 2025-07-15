using System;

namespace RecipeProject.Domain.Entities
{
    public class SearchHistory
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}