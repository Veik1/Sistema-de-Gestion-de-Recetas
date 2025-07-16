# Sistema de Gestión de Recetas

Este proyecto es un sistema completo para la gestión de recetas culinarias, permitiendo a los usuarios crear, consultar, comentar, calificar y reportar recetas. El sistema está construido sobre una arquitectura en capas y aprovecha contenedores para facilitar su despliegue.

---

## Tecnologías Utilizadas

- **Lenguaje principal:** C# (.NET 9)
- **Framework Backend:** ASP.NET Core
- **ORM:** Entity Framework Core
- **Base de datos:** PostgreSQL
- **Autenticación:** JWT (JSON Web Tokens)
- **Contenedores:** Docker, Docker Compose
- **Documentación API:** Swagger/OpenAPI

---

## Estructura del Proyecto

```
Sistema-de-Gestion-de-Recetas/
│
├── src/
│   ├── RecipeProject.Api/                # API REST principal
│   │   ├── Program.cs                    # Configuración de servicios, JWT, DB y Swagger
│   │   ├── Dockerfile                    # Dockerfile para la API
│   │   └── ...        
│   ├── RecipeProject.Application/        # Lógica de aplicación, interfaces y casos de uso
│   ├── RecipeProject.Domain/             # Entidades del dominio
│   └── RecipeProject.Infrastructure/     # Implementaciones de repositorios y acceso a datos
│
└── README.md
```

---

## Instalación y Configuración

### Requisitos previos

- [.NET 9 SDK](https://dotnet.microsoft.com/download) *(solo para desarrollo local sin Docker)*
- [Docker y Docker Compose](https://docs.docker.com/get-docker/)

---

### Opción 1: Despliegue con Docker Compose (Recomendado)

Levanta toda la solución (API + Base de Datos) con un solo comando, sin instalar nada extra en tu máquina.

1. Clona el repositorio:
    ```bash
    git clone https://github.com/Veik1/Sistema-de-Gestion-de-Recetas.git
    cd Sistema-de-Gestion-de-Recetas
    ```

2. Crea un archivo `.env` en la raíz del proyecto con el siguiente contenido (ajusta valores si lo deseas):

    ```
    POSTGRES_DB=RecipeDb
    POSTGRES_USER=postgres
    POSTGRES_PASSWORD=postgres
    JWT_KEY=random_key_OneTwoSix
    JWT_ISSUER=RecipeApi
    JWT_AUDIENCE=RecipeApiUsers
    ```

3. Asegúrate de tener el archivo `docker-compose.yml` (ejemplo incluido abajo).

4. Levanta todo el stack:
    ```bash
    docker-compose up --build
    ```

5. La API estará disponible en `http://localhost:8080` y la base de datos en `localhost:5432`.

---

#### Ejemplo de `docker-compose.yml`

```yaml
version: "3.9"
services:
  db:
    image: postgres:16
    restart: always
    environment:
      POSTGRES_DB: ${POSTGRES_DB}
      POSTGRES_USER: ${POSTGRES_USER}
      POSTGRES_PASSWORD: ${POSTGRES_PASSWORD}
    ports:
      - "5432:5432"
    volumes:
      - db_data:/var/lib/postgresql/data

  api:
    build: ./src/RecipeProject.Api
    depends_on:
      - db
    environment:
      - ConnectionStrings__DefaultConnection=Host=db;Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD}
      - Jwt__Key=${JWT_KEY}
      - Jwt__Issuer=${JWT_ISSUER}
      - Jwt__Audience=${JWT_AUDIENCE}
    ports:
      - "8080:8080"
    env_file:
      - .env

volumes:
  db_data:
```

---

### Opción 2: Desarrollo Local (sin Docker)

1. Clona el repositorio:
    ```bash
    git clone https://github.com/Veik1/Sistema-de-Gestion-de-Recetas.git
    cd Sistema-de-Gestion-de-Recetas/src/RecipeProject.Api
    ```

2. Crea una base de datos PostgreSQL local y obtén la cadena de conexión. Ejemplo:
    ```
    Host=localhost;Database=RecipeDb;Username=postgres;Password=postgres
    ```

3. Configura las variables de entorno necesarias (`Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`, `ConnectionStrings:DefaultConnection`) en `appsettings.json` o en tu entorno.

4. Aplica las migraciones:
    ```bash
    dotnet tool install --global dotnet-ef # solo la primera vez
    dotnet ef database update
    ```

5. Ejecuta la API:
    ```bash
    dotnet run
    ```

---

## Funcionalidades Principales

- CRUD de usuarios, recetas, categorías, ingredientes y comentarios
- Calificación y reportes de recetas
- Autenticación y autorización JWT
- Documentación interactiva de la API con Swagger
- Arquitectura en capas (Domain, Application, Infrastructure, Api)

---

## Extensión y Personalización

- Añade nuevas entidades y casos de uso en los proyectos Domain y Application.
- Implementa nuevas rutas o lógica en la API.
- Integra servicios externos (notificaciones, imágenes, etc.) usando la capa Infrastructure.

---

## Contribución

1. Haz un fork del repositorio.
2. Crea una rama (`git checkout -b feature/nueva-funcion`)
3. Realiza tus cambios y haz commit.
4. Abre un Pull Request.

---

## Licencia

Este proyecto está bajo la licencia MIT.

---

## Recursos y documentación

- [Documentación oficial de .NET](https://docs.microsoft.com/dotnet/)
- [Documentación de EF Core](https://learn.microsoft.com/ef/core/)
- [Swagger/OpenAPI](https://swagger.io/)

---

**Autor:** Veik1  
**Repositorio original:** https://github.com/Veik1/Sistema-de-Gestion-de-Recetas
