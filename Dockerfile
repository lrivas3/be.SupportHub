# ----------------------------
# 1) Build stage
# ----------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos archivos de solución y configuraciones de paquetes
COPY Directory.Build.props Directory.Packages.props global.json HelpPoint.sln ./

# Copiamos el csproj del proyecto y restauramos dependencias
COPY Src/HelpPoint/HelpPoint.csproj Src/HelpPoint/
RUN dotnet restore HelpPoint.sln

# Copiamos el resto del código fuente
COPY . .

# Publicamos en modo Release
WORKDIR /src/Src/HelpPoint
RUN dotnet publish \
    --configuration Release \
    --output /app/publish

# ----------------------------
# 2) Runtime stage
# ----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiamos los artefactos compilados desde la etapa build
COPY --from=build /app/publish .

# Exponemos el puerto 80 y configuramos la URL de la app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Punto de entrada
ENTRYPOINT ["dotnet", "HelpPoint.dll"]

