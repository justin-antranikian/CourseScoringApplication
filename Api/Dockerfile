# Use the official .NET 9 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY Api.csproj ./
RUN dotnet restore

# Copy the entire project and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET 9 runtime image for deployment
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/out ./

# Set the entry point
ENTRYPOINT ["dotnet", "Api.dll"]