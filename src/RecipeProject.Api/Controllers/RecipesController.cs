using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Application.UseCases;
using RecipeProject.Domain.Entities;
using System;

namespace RecipeProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly CreateRecipeUseCase _createRecipeUseCase;
        private readonly UpdateRecipeUseCase _updateRecipeUseCase;

        public RecipesController(
            IRecipeRepository recipeRepository,
            CreateRecipeUseCase createRecipeUseCase,
            UpdateRecipeUseCase updateRecipeUseCase)
        {
            _recipeRepository = recipeRepository;
            _createRecipeUseCase = createRecipeUseCase;
            _updateRecipeUseCase = updateRecipeUseCase;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_recipeRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var recipe = _recipeRepository.GetById(id);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Recipe recipe)
        {
            try
            {
                _createRecipeUseCase.Execute(recipe);
                return CreatedAtAction(nameof(GetById), new { id = recipe.Id }, recipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Recipe recipe)
        {
            if (id != recipe.Id) return BadRequest();
            try
            {
                _updateRecipeUseCase.Execute(recipe);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _recipeRepository.Delete(id);
            return NoContent();
        }
    }
}