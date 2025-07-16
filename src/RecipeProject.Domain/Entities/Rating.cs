using System;

namespace RecipeProject.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        private int _score;
        /// <summary>
        /// Score must be between 1 and 5.
        /// </summary>
        public int Score
        {
            get => _score;
            set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentOutOfRangeException(nameof(Score), "Score must be between 1 and 5.");
                _score = value;
            }
        }

        public string Review { get; set; }
        public DateTime Date { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}