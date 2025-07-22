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
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly UpdateIngredientUseCase _updateIngredientUseCase;

        public IngredientsController(
            IIngredientRepository ingredientRepository,
            UpdateIngredientUseCase updateIngredientUseCase)
        {
            _ingredientRepository = ingredientRepository;
            _updateIngredientUseCase = updateIngredientUseCase;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var ingredients = _ingredientRepository.GetAll();
            var dtos = ingredients.Select(MapIngredientToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ingredient = _ingredientRepository.GetById(id);
            if (ingredient == null) return NotFound();
            var dto = MapIngredientToDto(ingredient);
            return Ok(dto);
        }

        [HttpGet("by-recipe/{recipeId}")]
        public IActionResult GetByRecipeId(int recipeId)
        {
            var ingredients = _ingredientRepository.GetByRecipeId(recipeId);
            var dtos = ingredients.Select(MapIngredientToDto).ToList();
            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] IngredientDto ingredientDto)
        {
            var ingredient = MapDtoToIngredient(ingredientDto);
            _ingredientRepository.Add(ingredient);
            var dto = MapIngredientToDto(ingredient);
            return CreatedAtAction(nameof(GetById), new { id = ingredient.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] IngredientDto ingredientDto)
        {
            if (id != ingredientDto.Id) return BadRequest();
            var ingredient = MapDtoToIngredient(ingredientDto);
            _updateIngredientUseCase.Execute(ingredient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ingredientRepository.Delete(id);
            return NoContent();
        }

        // --- Manual mapping methods ---

        private static IngredientDto MapIngredientToDto(Ingredient ingredient)
        {
            return new IngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity
            };
        }

        private static Ingredient MapDtoToIngredient(IngredientDto dto)
        {
            return new Ingredient
            {
                Id = dto.Id,
                Name = dto.Name,
                Quantity = dto.Quantity
            };
        }
    }
}