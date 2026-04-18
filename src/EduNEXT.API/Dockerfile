FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EduNEXT.API/EduNEXT.API.csproj", "EduNEXT.API/"]
COPY ["EduNEXT.Application/EduNEXT.Application.csproj", "EduNEXT.Application/"]
COPY ["EduNEXT.Core/EduNEXT.Core.csproj", "EduNEXT.Core/"]
COPY ["utils/Primitives/Primitives.csproj", "utils/Primitives/"]
COPY ["EduNEXT.Infrastructure/EduNEXT.Infrastructure.csproj", "EduNEXT.Infrastructure/"]
RUN dotnet restore "EduNEXT.API/EduNEXT.API.csproj"
COPY . .
WORKDIR "/src/EduNEXT.API"
RUN dotnet build "./EduNEXT.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EduNEXT.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EduNEXT.API.dll"]
