using System.Collections.Generic;
using RecipeProject.Domain.Entities;

namespace RecipeProject.Application.Interfaces
{
    public interface IReportRepository
    {
        void Add(Report report);
        IEnumerable<Report> GetAll();
        IEnumerable<Report> GetUnresolved();
        void Resolve(int reportId);
    }
}