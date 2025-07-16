using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Domain.Entities;
using System;

namespace RecipeProject.Api.Controllers
{
    /// <summary>
    /// Endpoints for managing comments.
    /// </summary>
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

        /// <summary>
        /// Gets all comments.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll() => Ok(_commentRepository.GetAll());

        /// <summary>
        /// Gets a comment by its ID.
        /// </summary>
        /// <param name="id">Comment ID.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var comment = _commentRepository.GetById(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="comment">Comment data.</param>
        [HttpPost]
        public IActionResult Create([FromBody] Comment comment)
        {
            _commentRepository.Add(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
        }

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="id">Comment ID.</param>
        /// <param name="comment">Comment data.</param>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id) return BadRequest();
            _commentRepository.Update(comment);
            return NoContent();
        }

        /// <summary>
        /// Deletes a comment by ID.
        /// </summary>
        /// <param name="id">Comment ID.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _commentRepository.Delete(id);
            return NoContent();
        }
    }
}