FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

# Copy everything
COPY . .

RUN dotnet restore
RUN dotnet build -c Debug

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 80

ENTRYPOINT ["dotnet", "run", "--no-build"]