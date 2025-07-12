# Build stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DotNetTemplate.sln", "./"]
COPY ["src/Application/DotNetTemplate.Application.csproj", "src/Application/"]
COPY ["src/Domain/DotNetTemplate.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/DotNetTemplate.Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/WebApi/DotNetTemplate.WebApi.csproj", "src/WebApi/"]
RUN dotnet restore "src/WebApi/DotNetTemplate.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WebApi"
RUN dotnet publish "DotNetTemplate.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DotNetTemplate.WebApi.dll"]
