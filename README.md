# TheLibrary

API para la gestión de una biblioteca, desarrollada con ASP.NET Core, Entity Framework Core y PostgreSQL.

## Requisitos

- .NET SDK 10
- PostgreSQL disponible en `localhost:5432`
- La herramienta `dotnet-ef`

Para instalar `dotnet-ef`:

```bash
dotnet tool install --global dotnet-ef --version 10.0.13
```

## Configuración de la base de datos

La conexión se encuentra en `TheLibrary/appsettings.json` y utiliza la base de datos `the_library`.

Si todavía no existe, se puede crear desde PostgreSQL con:

```sql
CREATE DATABASE the_library;
```

## Primera ejecución

Restaurar las dependencias:

```bash
dotnet restore
```

Con PostgreSQL iniciado, aplicar las migraciones existentes:

```bash
dotnet ef database update \
  --project TheLibrary_DataAccessLayer \
  --startup-project TheLibrary
```

## Crear una nueva migración

Este paso se utiliza después de modificar las entidades o su configuración en Entity Framework Core. El nombre de la migración debe describir el cambio realizado.

Por ejemplo, para registrar la corrección de la relación entre usuarios y bibliotecas:

```bash
dotnet ef migrations add FixUserLibraryRelationship \
  --project TheLibrary_DataAccessLayer \
  --startup-project TheLibrary
```

Después, aplicar la nueva migración:

```bash
dotnet ef database update \
  --project TheLibrary_DataAccessLayer \
  --startup-project TheLibrary
```

## Ejecutar el proyecto

Desde la raíz del repositorio:

```bash
dotnet run --project TheLibrary/TheLibrary_PresentationLayer.csproj
```

En el entorno de desarrollo, Swagger queda disponible en:

```text
http://localhost:5076/swagger
```

