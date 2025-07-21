using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.DTOs;
using AutoMapper;
using System.Collections.Generic;

namespace RecipeProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public RatingsController(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var ratings = _ratingRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<RatingDto>>(ratings);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var rating = _ratingRepository.GetById(id);
            if (rating == null) return NotFound();
            var dto = _mapper.Map<RatingDto>(rating);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Rating rating)
        {
            _ratingRepository.Add(rating);
            var dto = _mapper.Map<RatingDto>(rating);
            return CreatedAtAction(nameof(GetById), new { id = rating.Id }, dto);
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