# Sistema de Gestión de Recetas

Proyecto personal desarrollado en C# (.NET), orientado a la gestión y administración de recetas culinarias. Permite crear, editar, listar y eliminar recetas, ingredientes y categorías, además de administrar usuarios y autenticación (si está habilitada).

---

## Tabla de contenido

- [Características principales](#características-principales)
- [Requisitos previos](#requisitos-previos)
- [Configuración inicial](#configuración-inicial)
- [Migraciones de la base de datos](#migraciones-de-la-base-de-datos)
- [Ejecución con Docker](#ejecución-con-docker)
- [Pruebas y uso con Swagger](#pruebas-y-uso-con-swagger)
- [Endpoints principales](#endpoints-principales)
- [Desarrollo](#desarrollo)
- [FAQ](#faq)
- [Licencia](#licencia)
- [Créditos](#créditos)

---

## Características principales

- CRUD completo de recetas, ingredientes y categorías.
- API RESTful basada en ASP.NET Core.
- Autenticación JWT (opcional, configurable desde `appsettings.json` y `appsettings.Development.json`).  
  > Si no configuras la sección `Jwt`, la autenticación estará deshabilitada.
- Documentación automática con Swagger.
- Despliegue sencillo usando Docker.
- Migraciones de base de datos con Entity Framework Core.

---

## Requisitos previos

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) o superior
- [Docker](https://www.docker.com/get-started)
- [Git](https://git-scm.com/)
- (Opcional) [Visual Studio](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)

---

## Configuración inicial

### 1. Clona el repositorio

```bash
git clone https://github.com/Veik1/Sistema-de-Gestion-de-Recetas.git
cd Sistema-de-Gestion-de-Recetas
```

### 2. Configura el archivo `appsettings.Development.json`

Crea el archivo `appsettings.Development.json` en la raíz del proyecto (junto a `appsettings.json`). Ejemplo de configuración:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=RecipeDb;Username=user;Password=1234"
  },
  "Jwt": {
    "Key": "dev_key_OneTwoSix_1234567890abcdef",
    "Issuer": "RecipeApiDev",
    "Audience": "RecipeApiUsersDev"
  }
}
```
> **Recuerda:** Cambia los datos de usuario, contraseña y nombre de la base según tu entorno.

---

## Migraciones de la base de datos

Este proyecto utiliza **Entity Framework Core** para el manejo de migraciones.

### Estructura típica de carpetas

- `src/RecipeProject.Infrastructure` — Contiene el contexto de datos y las migraciones.
- `src/RecipeProject.Api` — Proyecto API principal (el que arranca la aplicación).

### 1. Crear una migración nueva

```bash
dotnet ef migrations add Inicial \
  --project src/RecipeProject.Infrastructure \
  --startup-project src/RecipeProject.Api
```

### 2. Aplicar las migraciones a la base de datos

```bash
dotnet ef database update \
  --project src/RecipeProject.Infrastructure \
  --startup-project src/RecipeProject.Api
```

> **También puedes aplicar las migraciones desde Docker en el paso siguiente**

**Notas:**
- Repite el paso 1 cada vez que cambies el modelo de datos.
- Repite el paso 2 para aplicar los cambios en la base de datos.
- La estructura de carpetas puede variar según tu proyecto. Ajusta los paths si es necesario.

---

## Ejecución con Docker

### 1. Build & Run

```bash
docker build -t recetas-app .
docker run -d -p 8080:80 --name recetas-app recetas-app
```
> Cambia el puerto si lo necesitas.

### 2. Variables de entorno en Docker

Puedes pasar variables de conexión usando `-e` en el `docker run`:

```bash
docker run -d -p 8080:80 --name recetas-app \
    -e ConnectionStrings__DefaultConnection="Server=db;Database=RecetasDb;User Id=sa;Password=YourStrong!Passw0rd;" \
    recetas-app
```

### 3. Migraciones desde Docker

Si tienes el entorno corriendo en Docker y el contenedor tiene las herramientas de EF Core instaladas, ejecuta:

```bash
docker exec -it recetas-app dotnet ef database update \
  --project src/RecipeProject.Infrastructure \
  --startup-project src/RecipeProject.Api
```
> **Si le pusiste otro nombre al container, o dejaste el default, cambiar recetas-app por el nombre correspondiente**

---

## Pruebas y uso con Swagger

El proyecto expone la documentación Swagger en:

```
http://localhost:8080/swagger
```

---

## Endpoints principales

A continuación se muestran ejemplos de los principales endpoints expuestos por la API y ejemplos de JSON para Swagger.

### Recetas

- **GET /api/Recetas** — Lista todas las recetas

**Respuesta:**
```json
[
  {
    "id": 1,
    "nombre": "Ensalada César",
    "descripcion": "Clásica ensalada con pollo y crutones.",
    "ingredientes": [
      {
        "id": 1,
        "nombre": "Lechuga",
        "cantidad": "1 unidad"
      },
      {
        "id": 2,
        "nombre": "Pollo",
        "cantidad": "200g"
      }
    ],
    "categoria": {
      "id": 2,
      "nombre": "Ensaladas"
    }
  }
]
```

- **GET /api/Recetas/{id}** — Busca receta por ID

**Respuesta:**
```json
{
  "id": 1,
  "nombre": "Ensalada César",
  "descripcion": "Clásica ensalada con pollo y crutones.",
  "ingredientes": [
    {
      "id": 1,
      "nombre": "Lechuga",
      "cantidad": "1 unidad"
    },
    {
      "id": 2,
      "nombre": "Pollo",
      "cantidad": "200g"
    }
  ],
  "categoria": {
    "id": 2,
    "nombre": "Ensaladas"
  }
}
```

- **POST /api/Recetas** — Crea una receta

**JSON de ejemplo:**
```json
{
  "nombre": "Ensalada César",
  "descripcion": "Clásica ensalada con pollo y crutones.",
  "ingredientes": [
    {
      "nombre": "Lechuga",
      "cantidad": "1 unidad"
    },
    {
      "nombre": "Pollo",
      "cantidad": "200g"
    }
  ],
  "categoriaId": 2
}
```

- **PUT /api/Recetas/{id}** — Edita una receta

**JSON de ejemplo:**
```json
{
  "nombre": "Ensalada César Deluxe",
  "descripcion": "Ensalada César con extra pollo.",
  "ingredientes": [
    {
      "nombre": "Lechuga",
      "cantidad": "1 unidad"
    },
    {
      "nombre": "Pollo",
      "cantidad": "300g"
    },
    {
      "nombre": "Crutones",
      "cantidad": "50g"
    }
  ],
  "categoriaId": 2
}
```

- **DELETE /api/Recetas/{id}** — Elimina una receta

**Respuesta:**  
`204 No Content`

---

### Ingredientes

- **GET /api/Ingredientes** — Lista todos los ingredientes

**Respuesta:**
```json
[
  {
    "id": 1,
    "nombre": "Lechuga"
  },
  {
    "id": 2,
    "nombre": "Pollo"
  }
]
```

- **GET /api/Ingredientes/{id}** — Busca ingrediente por ID

**Respuesta:**
```json
{
  "id": 1,
  "nombre": "Lechuga"
}
```

- **POST /api/Ingredientes** — Crea un ingrediente

**JSON de ejemplo:**
```json
{
  "nombre": "Tomate"
}
```

- **PUT /api/Ingredientes/{id}** — Edita un ingrediente

**JSON de ejemplo:**
```json
{
  "nombre": "Tomate Cherry"
}
```

- **DELETE /api/Ingredientes/{id}** — Elimina un ingrediente

**Respuesta:**  
`204 No Content`

---

### Categorías

- **GET /api/Categorias** — Lista todas las categorías

**Respuesta:**
```json
[
  {
    "id": 1,
    "nombre": "Plato Principal"
  },
  {
    "id": 2,
    "nombre": "Ensaladas"
  }
]
```

- **GET /api/Categorias/{id}** — Busca categoría por ID

**Respuesta:**
```json
{
  "id": 2,
  "nombre": "Ensaladas"
}
```

- **POST /api/Categorias** — Crea una categoría

**JSON de ejemplo:**
```json
{
  "nombre": "Postres"
}
```

- **PUT /api/Categorias/{id}** — Edita una categoría

**JSON de ejemplo:**
```json
{
  "nombre": "Entradas"
}
```

- **DELETE /api/Categorias/{id}** — Elimina una categoría

**Respuesta:**  
`204 No Content`

---

> Puedes probar todos los endpoints y ejemplos directamente desde Swagger.

---

## Desarrollo

- Las capas principales están en las carpetas `Controllers`, `Models`, `Data` y `Services`.
- Puedes extender el modelo agregando nuevas migraciones y ejecutando `dotnet ef database update`.
- Para desarrollo local, usa el perfil `Development` y configura tu `appsettings.Development.json`.

---

## FAQ

- **¿Puedo usar otra base de datos?**  
  Sí, solo cambia la cadena de conexión y el proveedor de EF Core.
- **¿Cómo agrego una migración desde Docker?**  
  Usa `docker exec` como se mostró arriba.
- **¿Dónde veo la documentación de la API?**  
  En `/swagger` después de levantar el contenedor.

---

## Licencia

Este proyecto es de uso personal y educativo. Si deseas contribuir, ¡envía tu PR!

---

## Créditos

Desarrollado por [Veik1](https://github.com/Veik1).
