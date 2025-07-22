using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeProject.Application.DTOs
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        public bool IsGeneratedByAI { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public List<RecipeIngredientDto> Ingredients { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
