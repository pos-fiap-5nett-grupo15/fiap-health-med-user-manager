FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Fiap.Health.Med.User.Manager.Api/Fiap.Health.Med.User.Manager.Api.csproj", "src/Fiap.Health.Med.User.Manager.Api/"]
COPY ["src/Fiap.Health.Med.User.Manager.Application/Fiap.Health.Med.User.Manager.Application.csproj", "src/Fiap.Health.Med.User.Manager.Application/"]
COPY ["src/Fiap.Health.Med.User.Manager.Domain/Fiap.Health.Med.User.Manager.Domain.csproj", "src/Fiap.Health.Med.User.Manager.Domain/"]
COPY ["src/Fiap.Health.Med.User.Manager.Infrastructure/Fiap.Health.Med.User.Manager.Infrastructure.csproj", "src/Fiap.Health.Med.User.Manager.Infrastructure/"]
COPY ["src/Fiap.Health.Med.User.Manager.CrossCutting/Fiap.Health.Med.User.Manager.CrossCutting.csproj", "src/Fiap.Health.Med.User.Manager.CrossCutting/"]
RUN dotnet restore "src/Fiap.Health.Med.User.Manager.Api/Fiap.Health.Med.User.Manager.Api.csproj"
COPY . .
WORKDIR "/src/src/Fiap.Health.Med.User.Manager.Api"
RUN dotnet build "./Fiap.Health.Med.User.Manager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fiap.Health.Med.User.Manager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

USER root
COPY infrastructure/docker/wait-for-it.sh .
RUN chmod +x wait-for-it.sh
USER $APP_UID

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Health.Med.User.Manager.Api.dll"]
