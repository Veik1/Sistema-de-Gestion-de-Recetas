using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RecipeProject.Api.Controllers
{
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var reports = _reportRepository.GetAll();
            var dtos = reports.Select(MapReportToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var report = _reportRepository.GetAll().FirstOrDefault(r => r.Id == id);
            if (report == null) return NotFound();
            var dto = MapReportToDto(report);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ReportDto reportDto)
        {
            var report = MapDtoToReport(reportDto);
            _reportRepository.Add(report);
            var dto = MapReportToDto(report);
            return CreatedAtAction(nameof(GetById), new { id = report.Id }, dto);
        }

        [HttpPut("{id}/resolve")]
        public IActionResult Resolve(int id)
        {
            _reportRepository.Resolve(id);
            return NoContent();
        }

        // No delete method in IReportRepository

        // --- Manual mapping methods ---

        private static ReportDto MapReportToDto(Report report)
        {
            return new ReportDto
            {
                Id = report.Id,
                Reason = report.Reason,
                Date = report.Date,
                IsResolved = report.IsResolved,
                UserId = report.UserId,
                RecipeId = report.RecipeId,
                CommentId = report.CommentId
            };
        }

        private static Report MapDtoToReport(ReportDto dto)
        {
            return new Report
            {
                Id = dto.Id,
                Reason = dto.Reason,
                Date = dto.Date,
                IsResolved = dto.IsResolved,
                UserId = dto.UserId,
                RecipeId = dto.RecipeId,
                CommentId = dto.CommentId
            };
        }
    }
}