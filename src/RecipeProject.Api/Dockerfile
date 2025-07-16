# Dockerfile para .NET 9, Linux containers

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solo los csproj para aprovechar cache
COPY RecipeProject.Api/RecipeProject.Api.csproj RecipeProject.Api/
COPY ../RecipeProject.Application/RecipeProject.Application.csproj ../RecipeProject.Application/
COPY ../RecipeProject.Infrastructure/RecipeProject.Infrastructure.csproj ../RecipeProject.Infrastructure/
COPY ../RecipeProject.Domain/RecipeProject.Domain.csproj ../RecipeProject.Domain/
RUN dotnet restore "RecipeProject.Api/RecipeProject.Api.csproj"

# Copiar el resto del código
COPY . .
WORKDIR /src/RecipeProject.Api
RUN dotnet publish "RecipeProject.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app

# Variables de entorno para JWT (pueden ser sobreescritas por docker-compose/env)
ENV ASPNETCORE_URLS=http://+:8080

# Estas variables se pueden pasar en tiempo de ejecución (docker run/env/compose)
ENV Jwt__Key=""
ENV Jwt__Issuer=""
ENV Jwt__Audience=""

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RecipeProject.Api.dll"]
