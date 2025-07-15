using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using BCrypt.Net;

namespace RecipeProject.Application.UseCases
{
    public class LoginUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public LoginUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Execute(string email, string plainPassword)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(plainPassword, user.PasswordHash))
                throw new ArgumentException("Invalid email or password.");

            return user;
        }
    }
}