using System;
using System.Text.RegularExpressions;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class UpdateUserProfileUseCase
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserProfileUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(int userId, string newName, string newEmail)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new ArgumentException("User not found.");

            if (string.IsNullOrWhiteSpace(newName) || newName.Length > 100)
                throw new ArgumentException("Name is required and must be less than 100 characters.");

            if (!IsValidEmail(newEmail))
                throw new ArgumentException("Invalid email format.");

            var existingUser = _userRepository.GetByEmail(newEmail);
            if (existingUser != null && existingUser.Id != userId)
                throw new InvalidOperationException("Email is already registered.");

            if (user.Name == newName && user.Email == newEmail)
                return;

            user.Name = newName;
            user.Email = newEmail;

            _userRepository.Update(user);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}