# Etapa base: runtime de ASP.NET Core para .NET 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa build: SDK de .NET 9 para compilar la solución
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

ENV NUGET_PACKAGES=/src/.nuget/packages

COPY src/RecipeProject.Api/RecipeProject.Api.csproj RecipeProject.Api/
COPY src/RecipeProject.Application/RecipeProject.Application.csproj RecipeProject.Application/
COPY src/RecipeProject.Infrastructure/RecipeProject.Infrastructure.csproj RecipeProject.Infrastructure/
COPY src/RecipeProject.Domain/RecipeProject.Domain.csproj RecipeProject.Domain/

RUN dotnet restore RecipeProject.Api/RecipeProject.Api.csproj

COPY src/ ./

WORKDIR /src/RecipeProject.Api
RUN dotnet publish RecipeProject.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RecipeProject.Api.dll"]
