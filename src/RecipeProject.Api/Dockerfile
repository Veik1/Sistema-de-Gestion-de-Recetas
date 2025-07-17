# Etapa base: runtime de ASP.NET Core para .NET 9
# Dockerfile para .NET 9, Linux containers

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa build: SDK de .NET 9 para compilar la solución
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Usar una carpeta local para los paquetes NuGet
ENV NUGET_PACKAGES=/src/.nuget/packages

# Copia los archivos de proyecto desde el contexto
COPY src/RecipeProject.Api/RecipeProject.Api.csproj RecipeProject.Api/
COPY src/RecipeProject.Application/RecipeProject.Application.csproj RecipeProject.Application/
COPY src/RecipeProject.Infrastructure/RecipeProject.Infrastructure.csproj RecipeProject.Infrastructure/
COPY src/RecipeProject.Domain/RecipeProject.Domain.csproj RecipeProject.Domain/

# Restaura dependencias
RUN dotnet restore RecipeProject.Api/RecipeProject.Api.csproj

# Copia el resto del código fuente
COPY src/ ./

# Publica la aplicación en modo Release
WORKDIR /src/RecipeProject.Api
RUN dotnet publish RecipeProject.Api.csproj -c Release -o /app/publish

# Etapa final: imagen ligera lista para producción
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
