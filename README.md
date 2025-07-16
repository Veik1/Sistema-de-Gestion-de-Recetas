# Sistema de Gestión de Recetas

Este proyecto es un sistema completo para la gestión de recetas culinarias, permitiendo a los usuarios crear, consultar, comentar, calificar y reportar recetas. El sistema está construido sobre una arquitectura en capas y aprovecha contenedores **Linux** para facilitar su despliegue en cualquier sistema operativo, tanto Windows (usando Docker Desktop en modo Linux containers) como Linux nativo.

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
├── docker-compose.yml
├── .env
└── README.md
```

---

## Instalación y Configuración

### Requisitos previos

- [Docker y Docker Compose](https://docs.docker.com/get-docker/)
- *(Solo para desarrollo local sin Docker)* [.NET 9 SDK](https://dotnet.microsoft.com/download)

---

### Opción 1: Despliegue con Docker Compose (Recomendado)

Levanta toda la solución (API + Base de Datos) con un solo comando, sin instalar dependencias extra (excepto Docker y Compose).

#### **Usuarios de Windows**  
Usar [Docker Desktop](https://www.docker.com/products/docker-desktop/) en modo **Linux containers** (por defecto).

#### **Usuarios de Linux**  
Instala Docker y Docker Compose siguiendo la [guía oficial de Docker](https://docs.docker.com/engine/install/), y para Compose:  
- [Instalación de Docker Compose](https://docs.docker.com/compose/install/)

**Ejemplo rápido para Ubuntu:**
```bash
# Instalar Docker Engine
sudo apt update
sudo apt install -y apt-transport-https ca-certificates curl software-properties-common
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt update
sudo apt install -y docker-ce

# Instalar Docker Compose plugin
sudo apt install -y docker-compose-plugin

# Verifica instalación
docker --version
docker compose version
```
Más detalles en la [documentación oficial](https://docs.docker.com/engine/install/ubuntu/).

---

#### **Pasos para ambos sistemas**

1. **Clona el repositorio:**
    ```bash
    git clone https://github.com/Veik1/Sistema-de-Gestion-de-Recetas.git
    cd Sistema-de-Gestion-de-Recetas
    ```

2. **Crea un archivo `.env` en la raíz del proyecto:**
    ```
    POSTGRES_DB=RecipeDb
    POSTGRES_USER=postgres
    POSTGRES_PASSWORD=postgres
    JWT_KEY=random_key_OneTwoSix
    JWT_ISSUER=RecipeApi
    JWT_AUDIENCE=RecipeApiUsers
    ```

3. **Levanta todo el stack:**
    ```bash
    docker compose up --build
    ```
    - En versiones antiguas de Compose usa: `docker-compose up --build`

4. **Accede a la API y a la documentación interactiva:**  
   [http://localhost:8080/swagger](http://localhost:8080/swagger)  
   La base de datos estará en `localhost:5432`.

---

### Ejemplo de `docker-compose.yml`

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

- Añade nuevas entidades y casos de uso en Domain y Application.
- Implementa nuevas rutas o lógica en la API.
- Integra servicios externos usando Infrastructure.

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

- [Documentación oficial de Docker](https://docs.docker.com/get-docker/)
- [Documentación oficial de .NET](https://docs.microsoft.com/dotnet/)
- [Documentación de EF Core](https://learn.microsoft.com/ef/core/)
- [Swagger/OpenAPI](https://swagger.io/)

---

**Autor:** Veik1  
**Repositorio original:** https://github.com/Veik1/Sistema-de-Gestion-de-Recetas
