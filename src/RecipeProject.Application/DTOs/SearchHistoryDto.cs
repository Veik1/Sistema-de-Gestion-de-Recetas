using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeProject.Application.DTOs
{
    public class SearchHistoryDto
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
