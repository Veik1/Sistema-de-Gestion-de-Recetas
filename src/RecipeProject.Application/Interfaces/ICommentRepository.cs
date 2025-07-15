using System.Collections.Generic;
using RecipeProject.Domain.Entities;

namespace RecipeProject.Application.Interfaces
{
	public interface ICommentRepository
	{
		Comment GetById(int id);
		IEnumerable<Comment> GetAll();
		IEnumerable<Comment> GetByUserId(int userId);
		IEnumerable<Comment> GetByRecipeId(int recipeId);
		void Add(Comment comment);
		void Update(Comment comment);
		void Delete(int id);
	}
}