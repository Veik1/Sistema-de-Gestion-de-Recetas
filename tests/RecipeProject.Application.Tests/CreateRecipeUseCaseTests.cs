using Xunit;
using RecipeProject.Application.UseCases;
using RecipeProject.Domain.Entities;
using Moq;
using RecipeProject.Application.Interfaces;
using System;

public class CreateRecipeUseCaseTests
{
    [Fact]
    public void Execute_Throws_When_Title_Is_Empty()
    {
        var recipeRepo = new Mock<IRecipeRepository>();
        var userRepo = new Mock<IUserRepository>();
        var useCase = new CreateRecipeUseCase(recipeRepo.Object, userRepo.Object);

        var recipe = new Recipe { Title = "", Instructions = "abc", Ingredients = new(), UserId = 1 };

        Assert.Throws<ArgumentException>(() => useCase.Execute(recipe));
    }
}