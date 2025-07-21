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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();
            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }
    }
}