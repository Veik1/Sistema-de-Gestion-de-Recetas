# Usa argumentos para elegir plataforma base (Linux o Windows)
ARG BASE_IMAGE=mcr.microsoft.com/dotnet/aspnet:9.0
ARG SDK_IMAGE=mcr.microsoft.com/dotnet/sdk:9.0

# Imagen base (runtime)
FROM ${BASE_IMAGE} AS base
WORKDIR /app
EXPOSE 8080

# Imagen para build
FROM ${SDK_IMAGE} AS build
WORKDIR /src

COPY RecipeProject.Api/RecipeProject.Api.csproj RecipeProject.Api/
COPY ../RecipeProject.Application/RecipeProject.Application.csproj ../RecipeProject.Application/
COPY ../RecipeProject.Infrastructure/RecipeProject.Infrastructure.csproj ../RecipeProject.Infrastructure/
COPY ../RecipeProject.Domain/RecipeProject.Domain.csproj ../RecipeProject.Domain/
RUN dotnet restore "RecipeProject.Api/RecipeProject.Api.csproj"

COPY . .
WORKDIR /src/RecipeProject.Api
RUN dotnet publish "RecipeProject.Api.csproj" -c Release -o /app/publish --no-restore

# Imagen final
FROM base AS final
WORKDIR /app

ARG JWT_KEY
ARG JWT_ISSUER
ARG JWT_AUDIENCE

# Variables de entorno (usando sintaxis compatible para Windows y Linux)
ENV ASPNETCORE_URLS=http://+:8080
ENV Jwt__Key=${JWT_KEY}
ENV Jwt__Issuer=${JWT_ISSUER}
ENV Jwt__Audience=${JWT_AUDIENCE}

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RecipeProject.Api.dll"]
