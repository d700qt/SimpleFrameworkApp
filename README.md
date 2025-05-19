![Build and Deploy](https://github.com/timheuer/SimpleFrameworkApp/workflows/Build%20and%20Deploy/badge.svg?branch=master)

# SimpleFrameworkApp
Sample repo for showing how to use GitHub Actions for a .NET Framework app.

Blog post: https://timheuer.com/blog/building-net-framework-apps-using-github-actions/

## Set up on Windows 10 machine
- install git
- install msbuild tools
- install nuget and configure default public nuget source - nuget sources Add -Name 'nuget.org' -Source https://api.nuget.org/v3/index.json
- run nuget restore
- run msbuild -  msbuild .\SimpleFrameworkApp.sln /p:Configuration=Debug
- install IIS
- Configure site and app pool

## Running with Docker on Linux
This application has been migrated to .NET 8 and can be run in a Docker container:

### Prerequisites
- Docker installed on your machine
- Docker Compose (optional)

### Running with Docker Compose
```bash
# Build and start the container
docker-compose up --build

# Access the application at http://localhost:8080
```

### Running with Docker directly
```bash
# Build the Docker image
docker build -t simpleframeworkapp .

# Run the container
docker run -p 8080:80 -p 8443:443 simpleframeworkapp

# Access the application at http://localhost:8080
```