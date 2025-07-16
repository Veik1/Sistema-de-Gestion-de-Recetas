using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Application.UseCases;
using RecipeProject.Domain.Entities;
using System;

namespace RecipeProject.Api.Controllers
{
    /// <summary>
    /// Endpoints for managing categories.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UpdateCategoryUseCase _updateCategoryUseCase;

        public CategoriesController(ICategoryRepository categoryRepository, UpdateCategoryUseCase updateCategoryUseCase)
        {
            _categoryRepository = categoryRepository;
            _updateCategoryUseCase = updateCategoryUseCase;
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll() => Ok(_categoryRepository.GetAll());

        /// <summary>
        /// Gets a category by its ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category">Category data.</param>
        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            _categoryRepository.Add(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <param name="category">Category data.</param>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (id != category.Id) return BadRequest();
            _updateCategoryUseCase.Execute(category);
            return NoContent();
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return NoContent();
        }
    }
}