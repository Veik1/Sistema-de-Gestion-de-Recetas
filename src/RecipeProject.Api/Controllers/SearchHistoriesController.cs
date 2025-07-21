using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;

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
        private readonly IMapper _mapper;

        public SearchHistoriesController(ISearchHistoryRepository searchHistoryRepository, IMapper mapper)
        {
            _searchHistoryRepository = searchHistoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all search histories.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var histories = _searchHistoryRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<SearchHistoryDto>>(histories);
            return Ok(dtos);
        }

        /// <summary>
        /// Gets a search history by its ID.
        /// </summary>
        /// <param name="id">Search history ID.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var history = _searchHistoryRepository.GetById(id);
            if (history == null) return NotFound();
            var dto = _mapper.Map<SearchHistoryDto>(history);
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new search history entry.
        /// </summary>
        /// <param name="history">Search history data.</param>
        [HttpPost]
        public IActionResult Create([FromBody] SearchHistory history)
        {
            _searchHistoryRepository.Add(history);
            var dto = _mapper.Map<SearchHistoryDto>(history);
            return CreatedAtAction(nameof(GetById), new { id = history.Id }, dto);
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