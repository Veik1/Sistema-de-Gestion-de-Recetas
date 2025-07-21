using AutoMapper;
using RecipeProject.Application.DTOs;
using RecipeProject.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecipeProject.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Recipe, RecipeDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore()); // Ignora User al mapear de DTO a entidad
            CreateMap<Rating, RatingDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<SearchHistory, SearchHistoryDto>().ReverseMap();
        }
    }
}