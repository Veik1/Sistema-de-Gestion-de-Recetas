using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Application.UseCases;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace RecipeProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UpdateCategoryUseCase _updateCategoryUseCase;

        public CategoriesController(
            ICategoryRepository categoryRepository,
            UpdateCategoryUseCase updateCategoryUseCase)
        {
            _categoryRepository = categoryRepository;
            _updateCategoryUseCase = updateCategoryUseCase;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryRepository.GetAll();
            var dtos = categories.Select(MapCategoryToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return NotFound();
            var dto = MapCategoryToDto(category);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto categoryDto)
        {
            var category = MapDtoToCategory(categoryDto);
            _categoryRepository.Add(category);
            var dto = MapCategoryToDto(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryDto categoryDto)
        {
            if (id != categoryDto.Id) return BadRequest();
            var category = MapDtoToCategory(categoryDto);
            _updateCategoryUseCase.Execute(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return NoContent();
        }

        // --- Manual mapping methods ---

        private static CategoryDto MapCategoryToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Icon = category.Icon
            };
        }

        private static Category MapDtoToCategory(CategoryDto dto)
        {
            return new Category
            {
                Id = dto.Id,
                Name = dto.Name,
                Icon = dto.Icon
            };
        }
    }
}