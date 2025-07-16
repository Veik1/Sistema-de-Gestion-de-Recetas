using System.Collections.Generic;

namespace RecipeProject.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public List<Recipe> Recipes { get; set; } = new();
    }
}