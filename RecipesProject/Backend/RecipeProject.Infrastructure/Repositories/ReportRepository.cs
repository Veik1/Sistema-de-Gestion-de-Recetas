using System.Collections.Generic;
using System.Linq;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using RecipeProject.Infrastructure.Data;

namespace RecipeProject.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Report report)
        {
            _context.Reports.Add(report);
            _context.SaveChanges();
        }

        public IEnumerable<Report> GetAll() =>
            _context.Reports.ToList();

        public IEnumerable<Report> GetUnresolved() =>
            _context.Reports.Where(r => !r.IsResolved).ToList();

        public void Resolve(int reportId)
        {
            var report = _context.Reports.FirstOrDefault(r => r.Id == reportId);
            if (report != null)
            {
                report.IsResolved = true;
                _context.SaveChanges();
            }
        }
    }
}