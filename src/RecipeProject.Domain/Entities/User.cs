using System;
using System.Collections.Generic;

namespace RecipeProject.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }

        public List<Recipe> CreatedRecipes { get; set; } = new();
        public List<Recipe> FavoriteRecipes { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
        public List<SearchHistory> SearchHistories { get; set; } = new();
    }
}