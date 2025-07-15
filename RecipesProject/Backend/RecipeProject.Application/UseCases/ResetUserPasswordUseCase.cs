using System;
using System.Text.RegularExpressions;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using BCrypt.Net;

namespace RecipeProject.Application.UseCases
{
    public class ResetUserPasswordUseCase
    {
        private readonly IUserRepository _userRepository;

        public ResetUserPasswordUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(int userId, string newPassword)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new ArgumentException("User not found.");

            if (!IsStrongPassword(newPassword))
                throw new ArgumentException("Password must be at least 8 characters, contain a letter and a number.");

            if (BCrypt.Net.BCrypt.Verify(newPassword, user.PasswordHash))
                throw new ArgumentException("New password must be different from the current password.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepository.Update(user);
        }

        private bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;
            bool hasLetter = false, hasDigit = false;
            foreach (var c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                if (char.IsDigit(c)) hasDigit = true;
            }
            return hasLetter && hasDigit;
        }
    }
}