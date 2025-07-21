using System;
using System.Collections.Generic;

namespace RecipeProject.Domain.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        public bool IsGeneratedByAI { get; set; }
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }
}