# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy the solution and project files
COPY Ambev.sln ./
COPY Src/Core/Domain/Domain.csproj Src/Core/Domain/
COPY Src/Core/Application/Application.csproj Src/Core/Application/
COPY Src/Consumers/Api/Api.csproj Src/Consumers/Api/
COPY Src/Infrastructure/Persistence/pgSQL/pgSQL.csproj Src/Infrastructure/Persistence/pgSQL/
COPY Src/Infrastructure/Persistence/MongoData/MongoData.csproj Src/Infrastructure/Persistence/MongoData/
COPY Tests/Api/TestsApi.csproj Tests/Api/
COPY Tests/Application/TestsApplication.csproj Tests/Application/
COPY Tests/Domain/TestesDomain.csproj Tests/Domain/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build and publish the application
RUN dotnet publish -c Debug -o out

# Use the runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/out .

# ENV ASPNETCORE_ENVIRONMENT=Development
# ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "Api.dll"]