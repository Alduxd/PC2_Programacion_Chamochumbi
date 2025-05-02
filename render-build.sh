#!/usr/bin/env bash
# Exit on error
set -e

# Instalar dependencias y compilar la aplicación
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o out

# Copiar archivos compilados al directorio de despliegue
cp -a out/. $RENDER_APP_DIR

# Aplicar migraciones de base de datos
# Nota: Las migraciones se aplicarán automáticamente al iniciar la aplicación
# a través del código en Program.cs
