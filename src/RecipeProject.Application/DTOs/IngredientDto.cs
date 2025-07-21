using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeProject.Application.DTOs
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
    }
}
