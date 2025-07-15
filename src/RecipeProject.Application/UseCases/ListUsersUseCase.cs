using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Application.UseCases
{
	public class ListUsersUseCase
	{
		private readonly IUserRepository _userRepository;

		public ListUsersUseCase(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public IEnumerable<User> Execute()
		{
			return _userRepository.GetAll();
		}
	}
}