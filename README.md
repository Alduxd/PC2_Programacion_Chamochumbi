# Sistema de Adopción de Mascotas

Aplicación web para gestionar la adopción de mascotas, desarrollada con ASP.NET Core MVC y Entity Framework Core.

## Características

- Registro de mascotas disponibles para adopción
- Registro de personas interesadas en adoptar
- Asignación de mascotas a adoptantes
- Listado de todas las mascotas y sus adoptantes

## Tecnologías utilizadas

- ASP.NET Core 6.0
- Entity Framework Core 6.0
- SQL Server (desarrollo) / PostgreSQL (producción)
- Bootstrap 5

## Estructura del proyecto

El proyecto sigue la arquitectura MVC (Modelo-Vista-Controlador) y está organizado en las siguientes carpetas:

- `Controllers`: Controladores para manejar las solicitudes HTTP
- `Models`: Clases de modelo para representar los datos
- `Views`: Vistas para la interfaz de usuario
- `Data`: Contexto de base de datos y configuraciones
- `Migrations`: Migraciones de Entity Framework Core

## Despliegue en Render.com

Esta aplicación está configurada para ser desplegada en Render.com. Sigue estos pasos para desplegarla:

1. Crea una cuenta en Render.com si aún no tienes una
2. Crea una nueva base de datos PostgreSQL en Render.com
3. Conecta tu repositorio GitHub a Render.com
4. Configura un nuevo Web Service con las siguientes opciones:
   - **Build Command**: `chmod +x render-build.sh && ./render-build.sh`
   - **Start Command**: `cd $RENDER_APP_DIR && dotnet PetAdoptionApp.dll`
5. Configura las siguientes variables de entorno:
   - `DATABASE_URL`: La URL de conexión a tu base de datos PostgreSQL
   - `ASPNETCORE_ENVIRONMENT`: `Production`

## Desarrollo local

Para ejecutar la aplicación en tu entorno local:

1. Clona el repositorio
2. Asegúrate de tener instalado .NET 6.0 SDK
3. Configura la cadena de conexión en `appsettings.json`
4. Ejecuta las migraciones: `dotnet ef database update`
5. Inicia la aplicación: `dotnet run`
