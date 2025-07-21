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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var comments = _commentRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var comment = _commentRepository.GetById(id);
            if (comment == null) return NotFound();
            var dto = _mapper.Map<CommentDto>(comment);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Comment comment)
        {
            _commentRepository.Add(comment);
            var dto = _mapper.Map<CommentDto>(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id) return BadRequest();
            _commentRepository.Update(comment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _commentRepository.Delete(id);
            return NoContent();
        }
    }
}