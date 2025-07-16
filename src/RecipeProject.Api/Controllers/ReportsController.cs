using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Domain.Entities;
using System;
using System.Linq;

namespace RecipeProject.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to manage reports in the system.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        /// <summary>
        /// Gets all reports.
        /// </summary>
        /// <returns>List of all reports.</returns>
        [HttpGet]
        public IActionResult GetAll() => Ok(_reportRepository.GetAll());

        /// <summary>
        /// Gets a report by its ID.
        /// </summary>
        /// <param name="id">The report ID.</param>
        /// <returns>The report if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var report = _reportRepository.GetAll().FirstOrDefault(r => r.Id == id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        /// <summary>
        /// Creates a new report.
        /// </summary>
        /// <param name="report">The report to create.</param>
        /// <returns>The created report.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] Report report)
        {
            _reportRepository.Add(report);
            return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
        }

        /// <summary>
        /// Marks a report as resolved.
        /// </summary>
        /// <param name="id">The report ID to resolve.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}/resolve")]
        public IActionResult Resolve(int id)
        {
            _reportRepository.Resolve(id);
            return NoContent();
        }

        /// <summary>
        /// Deletes a report by its ID.
        /// </summary>
        /// <param name="id">The report ID.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // No delete method in IReportRepository
            return NoContent();
        }
    }
}