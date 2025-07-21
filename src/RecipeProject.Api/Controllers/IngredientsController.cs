using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Application.UseCases;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;

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
        private readonly IMapper _mapper;

        public IngredientsController(
            IIngredientRepository ingredientRepository,
            UpdateIngredientUseCase updateIngredientUseCase,
            IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _updateIngredientUseCase = updateIngredientUseCase;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all ingredients.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var ingredients = _ingredientRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
            return Ok(dtos);
        }

        /// <summary>
        /// Gets an ingredient by its ID.
        /// </summary>
        /// <param name="id">Ingredient ID.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ingredient = _ingredientRepository.GetById(id);
            if (ingredient == null) return NotFound();
            var dto = _mapper.Map<IngredientDto>(ingredient);
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new ingredient.
        /// </summary>
        /// <param name="ingredient">Ingredient data.</param>
        [HttpPost]
        public IActionResult Create([FromBody] Ingredient ingredient)
        {
            _ingredientRepository.Add(ingredient);
            var dto = _mapper.Map<IngredientDto>(ingredient);
            return CreatedAtAction(nameof(GetById), new { id = ingredient.Id }, dto);
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