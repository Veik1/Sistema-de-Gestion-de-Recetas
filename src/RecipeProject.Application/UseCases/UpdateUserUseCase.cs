using System;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class UpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(User updatedUser)
        {
            var existingUser = _userRepository.GetById(updatedUser.Id);
            if (existingUser == null)
                throw new ArgumentException("User does not exist.");

            if (string.IsNullOrWhiteSpace(updatedUser.Name))
                throw new ArgumentException("Name is required.");

            // Optional: Validate email, etc.

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;

            _userRepository.Update(existingUser);
        }
    }
}