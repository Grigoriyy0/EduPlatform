FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# copy project files for restore
COPY src/EduNEXT.API/EduNEXT.API.csproj src/EduNEXT.API/
COPY src/EduNEXT.Application/EduNEXT.Application.csproj src/EduNEXT.Application/
COPY src/EduNEXT.Core/EduNEXT.Core.csproj src/EduNEXT.Core/
COPY src/EduNEXT.Infrastructure/EduNEXT.Infrastructure.csproj src/EduNEXT.Infrastructure/
COPY src/utils/Primitives/Primitives.csproj src/utils/Primitives/
RUN dotnet restore "src/EduNEXT.API/EduNEXT.API.csproj"
# copy full source
COPY src/ ./src/
WORKDIR "/src/src/EduNEXT.API"
RUN dotnet build "./EduNEXT.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EduNEXT.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EduNEXT.API.dll"]
