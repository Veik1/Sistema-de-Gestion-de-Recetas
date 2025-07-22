using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace RecipeProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingsController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var ratings = _ratingRepository.GetAll();
            var dtos = ratings.Select(MapRatingToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var rating = _ratingRepository.GetById(id);
            if (rating == null) return NotFound();
            var dto = MapRatingToDto(rating);
            return Ok(dto);
        }

        [HttpGet("by-recipe/{recipeId}")]
        public IActionResult GetByRecipeId(int recipeId)
        {
            var ratings = _ratingRepository.GetByRecipeId(recipeId);
            var dtos = ratings.Select(MapRatingToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("by-user/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var ratings = _ratingRepository.GetByUserId(userId);
            var dtos = ratings.Select(MapRatingToDto).ToList();
            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] RatingDto ratingDto)
        {
            var rating = MapDtoToRating(ratingDto);
            _ratingRepository.Add(rating);
            var dto = MapRatingToDto(rating);
            return CreatedAtAction(nameof(GetById), new { id = rating.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RatingDto ratingDto)
        {
            if (id != ratingDto.Id) return BadRequest();
            var rating = MapDtoToRating(ratingDto);
            _ratingRepository.Update(rating);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ratingRepository.Delete(id);
            return NoContent();
        }

        // --- Manual mapping methods ---

        private static RatingDto MapRatingToDto(Rating rating)
        {
            return new RatingDto
            {
                Id = rating.Id,
                Score = rating.Score,
                Review = rating.Review,
                Date = rating.Date,
                UserId = rating.UserId,
                UserName = rating.User?.Name
            };
        }

        private static Rating MapDtoToRating(RatingDto dto)
        {
            return new Rating
            {
                Id = dto.Id,
                Score = dto.Score,
                Review = dto.Review,
                Date = dto.Date,
                UserId = dto.UserId
            };
        }
    }
}