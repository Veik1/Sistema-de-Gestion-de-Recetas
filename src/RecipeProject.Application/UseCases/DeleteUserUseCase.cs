using System;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
    public class DeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new ArgumentException("User not found.");

            _userRepository.Delete(userId);
        }
    }
}