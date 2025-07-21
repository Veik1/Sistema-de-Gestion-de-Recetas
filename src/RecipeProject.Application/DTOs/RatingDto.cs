using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeProject.Application.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Review { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
