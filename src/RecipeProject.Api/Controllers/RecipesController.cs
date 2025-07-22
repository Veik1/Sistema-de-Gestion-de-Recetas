using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RecipeProject.Application.Interfaces;
using RecipeProject.Application.UseCases;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly CreateRecipeUseCase _createRecipeUseCase;
        private readonly UpdateRecipeUseCase _updateRecipeUseCase;

        public RecipesController(
            IRecipeRepository recipeRepository,
            ICategoryRepository categoryRepository,
            IIngredientRepository ingredientRepository,
            CreateRecipeUseCase createRecipeUseCase,
            UpdateRecipeUseCase updateRecipeUseCase)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _ingredientRepository = ingredientRepository;
            _createRecipeUseCase = createRecipeUseCase;
            _updateRecipeUseCase = updateRecipeUseCase;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var recipes = _recipeRepository.GetAll();
            var dtos = recipes.Select(MapRecipeToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var recipe = _recipeRepository.GetById(id);
            if (recipe == null) return NotFound();
            var dto = MapRecipeToDto(recipe);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] RecipeDto recipeDto)
        {
            Console.WriteLine($"Ingredients null? {recipeDto.Ingredients == null}");
            Console.WriteLine($"Ingredients count: {recipeDto.Ingredients?.Count}");
            try
            {
                if (recipeDto == null)
                    return BadRequest("Recipe data is required.");

                var normalizedTitle = recipeDto.Title?.Trim().ToLowerInvariant();
                if (string.IsNullOrWhiteSpace(normalizedTitle))
                    return BadRequest("Recipe title is required.");

                if (_recipeRepository.GetAll().Any(r => r.Title.Trim().ToLowerInvariant() == normalizedTitle))
                    return BadRequest("A recipe with this name already exists.");

                var recipe = MapDtoToRecipe(recipeDto, _categoryRepository, _ingredientRepository);
                _createRecipeUseCase.Execute(recipe);
                var dto = MapRecipeToDto(recipe);
                return CreatedAtAction(nameof(GetById), new { id = recipe.Id }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RecipeDto recipeDto)
        {
            if (recipeDto == null)
                return BadRequest("Recipe data is required.");
            if (id != recipeDto.Id)
                return BadRequest("Recipe ID mismatch.");

            try
            {
                var normalizedTitle = recipeDto.Title?.Trim().ToLowerInvariant();
                if (string.IsNullOrWhiteSpace(normalizedTitle))
                    return BadRequest("Recipe title is required.");

                if (_recipeRepository.GetAll().Any(r => r.Id != id && r.Title.Trim().ToLowerInvariant() == normalizedTitle))
                    return BadRequest("A recipe with this name already exists.");

                var recipe = MapDtoToRecipe(recipeDto, _categoryRepository, _ingredientRepository);
                _updateRecipeUseCase.Execute(recipe);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _recipeRepository.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // --- Manual mapping methods ---

        private static RecipeDto MapRecipeToDto(Recipe recipe)
        {
            return new RecipeDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                IsGeneratedByAI = recipe.IsGeneratedByAI,
                CreationDate = recipe.CreationDate,
                UserId = recipe.UserId,
                Ingredients = recipe.RecipeIngredients?.Select(ri => new RecipeIngredientDto
                {
                    IngredientId = ri.IngredientId,
                    IngredientName = ri.Ingredient?.Name,
                    Quantity = ri.Quantity
                }).ToList() ?? new List<RecipeIngredientDto>(),
                Categories = recipe.Categories?.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Icon = c.Icon
                }).ToList() ?? new List<CategoryDto>()
            };
        }

        /// <summary>
        /// Mapea el DTO a la entidad Recipe, evitando duplicados de ingredientes y categorías.
        /// - Si el ingrediente/categoría existe (por id o nombre), lo asocia.
        /// - Si no existe, lo crea solo si no hay otro con el mismo nombre (case-insensitive).
        /// - Valida unicidad global y por receta.
        /// </summary>
        private static Recipe MapDtoToRecipe(
            RecipeDto dto,
            ICategoryRepository categoryRepository,
            IIngredientRepository ingredientRepository)
        {
            var recipeIngredients = new List<RecipeIngredient>();
            var ingredientNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (dto.Ingredients != null)
            {
                foreach (var i in dto.Ingredients)
                {
                    Ingredient ingredient = null;
                    if (i.IngredientId > 0)
                    {
                        ingredient = ingredientRepository.GetById(i.IngredientId);
                        if (ingredient == null)
                            throw new ArgumentException($"Ingredient with id {i.IngredientId} not found.");
                    }
                    else if (!string.IsNullOrWhiteSpace(i.IngredientName))
                    {
                        var normalizedName = i.IngredientName.Trim().ToLowerInvariant();
                        ingredient = ingredientRepository.GetAll()
                            .FirstOrDefault(x => x.Name.Trim().ToLowerInvariant() == normalizedName);

                        if (ingredient == null)
                        {
                            if (ingredientRepository.GetAll().Any(x => x.Name.Trim().ToLowerInvariant() == normalizedName))
                                throw new ArgumentException($"An ingredient with the name '{i.IngredientName.Trim()}' already exists.");

                            ingredient = new Ingredient { Name = i.IngredientName.Trim() };
                            ingredientRepository.Add(ingredient);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Ingredient name is required for new ingredients.");
                    }

                    if (!ingredientNames.Add(ingredient.Name.Trim().ToLowerInvariant()))
                        throw new ArgumentException($"Duplicate ingredient '{ingredient.Name}' in recipe.");

                    recipeIngredients.Add(new RecipeIngredient
                    {
                        Ingredient = ingredient,
                        IngredientId = ingredient.Id,
                        Quantity = i.Quantity
                    });
                }
            }

            var categories = new List<Category>();
            var categoryNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (dto.Categories != null)
            {
                foreach (var c in dto.Categories)
                {
                    Category category = null;
                    if (c.Id > 0)
                    {
                        category = categoryRepository.GetById(c.Id);
                    }
                    if (category == null && !string.IsNullOrWhiteSpace(c.Name))
                    {
                        var normalizedCat = c.Name.Trim().ToLowerInvariant();
                        category = categoryRepository.GetAll()
                            .FirstOrDefault(x => x.Name.Trim().ToLowerInvariant() == normalizedCat);

                        if (category == null)
                        {
                            if (categoryRepository.GetAll().Any(x => x.Name.Trim().ToLowerInvariant() == normalizedCat))
                                throw new ArgumentException($"A category with the name '{c.Name.Trim()}' already exists.");

                            category = new Category { Name = c.Name.Trim(), Icon = c.Icon };
                            categoryRepository.Add(category);
                        }
                    }
                    if (category != null && categoryNames.Add(category.Name.Trim().ToLowerInvariant()))
                        categories.Add(category);
                }
            }

            return new Recipe
            {
                Id = dto.Id,
                Title = dto.Title,
                Instructions = dto.Instructions,
                ImageUrl = dto.ImageUrl,
                IsGeneratedByAI = dto.IsGeneratedByAI,
                CreationDate = dto.CreationDate,
                UserId = dto.UserId,
                RecipeIngredients = recipeIngredients,
                Ingredients = recipeIngredients.Select(ri => ri.Ingredient).ToList(), // <--- SOLUCIÓN
                Categories = categories
            };
        }
    }
}