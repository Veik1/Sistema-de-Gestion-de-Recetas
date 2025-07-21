using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeProject.Application.DTOs
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public bool IsResolved { get; set; }
        public int? UserId { get; set; }
        public int? RecipeId { get; set; }
        public int? CommentId { get; set; }
    }
}