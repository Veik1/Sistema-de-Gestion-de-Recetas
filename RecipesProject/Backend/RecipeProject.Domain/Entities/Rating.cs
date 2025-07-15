namespace RecipeProject.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; } // 1-5

        public int UserId { get; set; }
        public User User { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}