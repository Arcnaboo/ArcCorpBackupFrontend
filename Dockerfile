# Stage 1: Build with .NET 9 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and restore dependencies
COPY . .
RUN dotnet restore ArcCorpBackupFrontend.sln

# Publish Blazor WASM app
RUN dotnet publish ArcCorpBackupFrontend.sln -c Release -o /app/publish

# Stage 2: Serve with Nginx
FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Expose default HTTP port
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
