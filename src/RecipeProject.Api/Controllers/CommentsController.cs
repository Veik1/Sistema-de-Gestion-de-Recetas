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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var comments = _commentRepository.GetAll();
            var dtos = comments.Select(MapCommentToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var comment = _commentRepository.GetById(id);
            if (comment == null) return NotFound();
            var dto = MapCommentToDto(comment);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CommentDto commentDto)
        {
            var comment = MapDtoToComment(commentDto);
            _commentRepository.Add(comment);
            var dto = MapCommentToDto(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id) return BadRequest();
            var comment = MapDtoToComment(commentDto);
            _commentRepository.Update(comment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _commentRepository.Delete(id);
            return NoContent();
        }

        // --- Manual mapping methods ---

        private static CommentDto MapCommentToDto(Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Text = comment.Text,
                Date = comment.Date,
                UserId = comment.UserId,
                UserName = comment.User?.Name
            };
        }

        private static Comment MapDtoToComment(CommentDto dto)
        {
            return new Comment
            {
                Id = dto.Id,
                Text = dto.Text,
                Date = dto.Date,
                UserId = dto.UserId
                // User is not set here, only UserId
            };
        }
    }
}