using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Application.UseCases;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.DTOs;
using AutoMapper;
using System.Collections.Generic;

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
        private readonly IMapper _mapper;

        public CategoriesController(
            ICategoryRepository categoryRepository,
            UpdateCategoryUseCase updateCategoryUseCase,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _updateCategoryUseCase = updateCategoryUseCase;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(dtos);
        }

        /// <summary>
        /// Gets a category by its ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return NotFound();
            var dto = _mapper.Map<CategoryDto>(category);
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category">Category data.</param>
        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            _categoryRepository.Add(category);
            var dto = _mapper.Map<CategoryDto>(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, dto);
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