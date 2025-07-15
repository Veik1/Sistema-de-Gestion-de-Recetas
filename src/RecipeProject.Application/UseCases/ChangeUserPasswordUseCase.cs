using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using BCrypt.Net;

namespace RecipeProject.Application.UseCases
{
    public class ChangeUserPasswordUseCase
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserPasswordUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(int userId, string currentPassword, string newPassword)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new ArgumentException("User not found.");

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                throw new ArgumentException("Current password is incorrect.");

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
                throw new ArgumentException("New password must be at least 8 characters.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepository.Update(user);
        }
    }
}