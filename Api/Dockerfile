ARG DOTNET_VERSION=9.0

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_VERSION AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY Api.csproj ./
RUN dotnet restore

# Copy the entire project and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for deployment
FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_VERSION AS runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/out ./

# Set the entry point
ENTRYPOINT ["dotnet", "Api.dll"]