# Imagen base oficial de .NET 9 para aplicaciones ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagen para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia los archivos de solución y restaura dependencias
COPY ["RecipeProject.Api/RecipeProject.Api.csproj", "RecipeProject.Api/"]
COPY ["../RecipeProject.Application/RecipeProject.Application.csproj", "../RecipeProject.Application/"]
COPY ["../RecipeProject.Infrastructure/RecipeProject.Infrastructure.csproj", "../RecipeProject.Infrastructure/"]
COPY ["../RecipeProject.Domain/RecipeProject.Domain.csproj", "../RecipeProject.Domain/"]

RUN dotnet restore "RecipeProject.Api/RecipeProject.Api.csproj"

# Copia el resto del código fuente
COPY . .

WORKDIR "/src/RecipeProject.Api"
RUN dotnet publish "RecipeProject.Api.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RecipeProject.Api.dll"]