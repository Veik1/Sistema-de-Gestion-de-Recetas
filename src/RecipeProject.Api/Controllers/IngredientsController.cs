using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Application.UseCases;
using RecipeProject.Domain.Entities;
using System;

namespace RecipeProject.Api.Controllers
{
    /// <summary>
    /// Endpoints for managing ingredients.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly UpdateIngredientUseCase _updateIngredientUseCase;

        public IngredientsController(IIngredientRepository ingredientRepository, UpdateIngredientUseCase updateIngredientUseCase)
        {
            _ingredientRepository = ingredientRepository;
            _updateIngredientUseCase = updateIngredientUseCase;
        }

        /// <summary>
        /// Gets all ingredients.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll() => Ok(_ingredientRepository.GetAll());

        /// <summary>
        /// Gets an ingredient by its ID.
        /// </summary>
        /// <param name="id">Ingredient ID.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ingredient = _ingredientRepository.GetById(id);
            if (ingredient == null) return NotFound();
            return Ok(ingredient);
        }

        /// <summary>
        /// Creates a new ingredient.
        /// </summary>
        /// <param name="ingredient">Ingredient data.</param>
        [HttpPost]
        public IActionResult Create([FromBody] Ingredient ingredient)
        {
            _ingredientRepository.Add(ingredient);
            return CreatedAtAction(nameof(GetById), new { id = ingredient.Id }, ingredient);
        }

        /// <summary>
        /// Updates an existing ingredient.
        /// </summary>
        /// <param name="id">Ingredient ID.</param>
        /// <param name="ingredient">Ingredient data.</param>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Ingredient ingredient)
        {
            if (id != ingredient.Id) return BadRequest();
            _updateIngredientUseCase.Execute(ingredient);
            return NoContent();
        }

        /// <summary>
        /// Deletes an ingredient by ID.
        /// </summary>
        /// <param name="id">Ingredient ID.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ingredientRepository.Delete(id);
            return NoContent();
        }
    }
}