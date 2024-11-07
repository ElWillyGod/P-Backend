# Willinn-Backend

## Descripción

Willinn-Backend es un proyecto de backend desarrollado en .NET Core. Este proyecto incluye varias capas como API, Servicios, Datos y Core, cada una con responsabilidades específicas. Proporciona funcionalidades CRUD (Crear, Leer, Actualizar, Eliminar) para la gestión de usuarios.

## Dependencias

- [C# 12.0](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12)
- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Estructura del Proyecto

- **Agents/**: Contiene los agentes del sistema.
- **Api/**: Contiene la API del proyecto.
  - **Controllers/**: Controladores de la API.
  - **Extensions/**: Extensiones y configuraciones adicionales.
  - **Properties/**: Configuraciones de lanzamiento.
  - **appsettings.json**: Configuraciones de la aplicación.
  - **Dockerfile**: Archivo Docker para construir la imagen de la API.
  - **Dockerfile-test**: Archivo Docker para pruebas.
- **Core/**: Contiene las interfaces y modelos del dominio.
  - **Interfaces/**: Interfaces del dominio.
  - **User.cs**: Modelo de usuario.
- **Data/**: Contiene la lógica de acceso a datos.
  - **DbContext.cs**: Contexto de la base de datos.
  - **UserRepository.cs**: Repositorio de usuarios.
- **Services/**: Contiene los servicios de la aplicación.
- **UnitTests/**: Contiene las pruebas unitarias.
- **docker-compose.yml**: Archivo de configuración para Docker Compose.
- **global.json**: Configuración global del proyecto.

## Funcionalidades CRUD

El proyecto proporciona las siguientes funcionalidades CRUD para la gestión de usuarios:

- **Crear Usuario**: Permite crear un nuevo usuario.
- **Leer Usuarios**: Permite obtener la lista de todos los usuarios o un usuario específico por su ID.
- **Actualizar Usuario**: Permite actualizar la información de un usuario existente.
- **Eliminar Usuario**: Permite eliminar (desactivar) un usuario por su ID.

## Ejecución del Proyecto

1. Clona el repositorio:

    ```sh
    git clone https://github.com/tu-usuario/willinn-backend.git
    cd willinn-backend
    ```

2. Restaura las dependencias:

    ```sh
    dotnet restore
    ```

3. Configura las variables de entorno en `appsettings.Development.json` y `appsettings.json`.

4. Ejecuta el proyecto:

    ```sh
    dotnet run --project Api/Api.csproj
    ```

5. Para ejecutar en Docker:

    ```sh
    docker-compose up
    ```