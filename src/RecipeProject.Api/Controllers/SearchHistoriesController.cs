using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace RecipeProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SearchHistoriesController : ControllerBase
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public SearchHistoriesController(ISearchHistoryRepository searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var histories = _searchHistoryRepository.GetAll();
            var dtos = histories.Select(MapSearchHistoryToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var history = _searchHistoryRepository.GetById(id);
            if (history == null) return NotFound();
            var dto = MapSearchHistoryToDto(history);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SearchHistoryDto dto)
        {
            var history = MapDtoToSearchHistory(dto);
            _searchHistoryRepository.Add(history);
            var resultDto = MapSearchHistoryToDto(history);
            return CreatedAtAction(nameof(GetById), new { id = history.Id }, resultDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _searchHistoryRepository.Delete(id);
            return NoContent();
        }

        // --- Manual mapping methods ---

        private static SearchHistoryDto MapSearchHistoryToDto(SearchHistory history)
        {
            return new SearchHistoryDto
            {
                Id = history.Id,
                Query = history.Query,
                Date = history.Date,
                UserId = history.UserId
            };
        }

        private static SearchHistory MapDtoToSearchHistory(SearchHistoryDto dto)
        {
            return new SearchHistory
            {
                Id = dto.Id,
                Query = dto.Query,
                Date = dto.Date,
                UserId = dto.UserId
            };
        }
    }
}