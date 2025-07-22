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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll();
            var dtos = users.Select(MapUserToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();
            var dto = MapUserToDto(user);
            return Ok(dto);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] UserDto userDto)
        {
            var user = MapDtoToUser(userDto);
            _userRepository.Add(user);
            var dto = MapUserToDto(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id) return BadRequest();
            var user = MapDtoToUser(userDto);
            _userRepository.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userRepository.Delete(id);
            return NoContent();
        }

        // --- Manual mapping methods ---

        private static UserDto MapUserToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate
            };
        }

        private static User MapDtoToUser(UserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                RegistrationDate = dto.RegistrationDate
            };
        }
    }
}