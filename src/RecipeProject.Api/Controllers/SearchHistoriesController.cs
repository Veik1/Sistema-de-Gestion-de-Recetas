using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Domain.Entities;
using System;

namespace RecipeProject.Api.Controllers
{
    /// <summary>
    /// Endpoints for managing user search histories.
    /// </summary>
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


        /// <summary>
        /// Gets all search histories.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll() => Ok(_searchHistoryRepository.GetAll());

        /// <summary>
        /// Gets a search history by its ID.
        /// </summary>
        /// <param name="id">Search history ID.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var history = _searchHistoryRepository.GetById(id);
            if (history == null) return NotFound();
            return Ok(history);
        }

        /// <summary>
        /// Creates a new search history entry.
        /// </summary>
        /// <param name="history">Search history data.</param>
        [HttpPost]
        public IActionResult Create([FromBody] SearchHistory history)
        {
            _searchHistoryRepository.Add(history);
            return CreatedAtAction(nameof(GetById), new { id = history.Id }, history);
        }

        /// <summary>
        /// Deletes a search history entry by ID.
        /// </summary>
        /// <param name="id">Search history ID.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _searchHistoryRepository.Delete(id);
            return NoContent();
        }
    }
}