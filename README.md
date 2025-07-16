# Sistema de Gestión de Recetas

Este proyecto es un sistema completo para la gestión de recetas culinarias, permitiendo a los usuarios crear, consultar, comentar, calificar y reportar recetas. El sistema está construido sobre una arquitectura moderna con C#, ASP.NET Core, Entity Framework Core y PostgreSQL, y preparado para despliegue en contenedores Docker.

---

## Tecnologías Utilizadas

- **Lenguaje principal:** C# (.NET 9)
- **Framework Backend:** ASP.NET Core
- **ORM:** Entity Framework Core
- **Base de datos:** PostgreSQL
- **Autenticación:** JWT (JSON Web Tokens)
- **Contenedores:** Docker
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

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [PostgreSQL](https://www.postgresql.org/) (o usar Docker para la base de datos)

### 1. Clonar el repositorio

```bash
git clone https://github.com/Veik1/Sistema-de-Gestion-de-Recetas.git
cd Sistema-de-Gestion-de-Recetas/src/RecipeProject.Api
```

### 2. Configuración de la base de datos

Crea una base de datos PostgreSQL y asegúrate de tener la cadena de conexión. Por ejemplo:

```
Host=localhost;Database=recetas;Username=usuario;Password=contraseña
```

Agrega esta cadena en `appsettings.json` o en las variables de entorno del servicio.

### 3. Variables de entorno

- `Jwt:Key`: Clave secreta para JWT
- `Jwt:Issuer`: Emisor del token
- `Jwt:Audience`: Audiencia del token
- `ConnectionStrings:DefaultConnection`: Cadena de conexión a PostgreSQL

Puede establecerse en el entorno o en el archivo de configuración.

### 4. Migrar la base de datos

Desde la carpeta de la API:

```bash
dotnet ef database update
```

(Asegúrate de tener instalado el CLI de EF: `dotnet tool install --global dotnet-ef`)

---

## Ejecución en Desarrollo

```bash
dotnet run
```

La API estará disponible en `http://localhost:8080` (según configuración del Dockerfile y launchSettings).

---

## Uso con Docker

### 1. Construir la imagen

```bash
docker build -t recetas-api .
```

### 2. Ejecutar el contenedor

```bash
docker run -d -p 8080:8080 --env-file .env recetas-api
```

Crea un archivo `.env` con las variables de entorno necesarias.

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
