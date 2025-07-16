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
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly CreateRatingUseCase _createRatingUseCase;

        public RatingsController(IRatingRepository ratingRepository, CreateRatingUseCase createRatingUseCase)
        {
            _ratingRepository = ratingRepository;
            _createRatingUseCase = createRatingUseCase;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_ratingRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var rating = _ratingRepository.GetById(id);
            if (rating == null) return NotFound();
            return Ok(rating);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Rating rating)
        {
            try
            {
                _createRatingUseCase.Execute(rating);
                return CreatedAtAction(nameof(GetById), new { id = rating.Id }, rating);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Rating rating)
        {
            if (id != rating.Id) return BadRequest();
            _ratingRepository.Update(rating);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ratingRepository.Delete(id);
            return NoContent();
        }
    }
}