# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

 
EXPOSE 8080
EXPOSE 8081

ENV HOST_DB=localhost
ENV PORT_DB=5432
ENV DB_NAME=UserDb
ENV USER_DB=postgres
ENV PASS_DB=1213 
ENV ASPNETCORESTAGE=Realase 


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ENV PATH="${PATH}:/root/.dotnet/tools" 

WORKDIR /src
COPY ["OtusMiniApi/OtusMiniApi.csproj", "OtusMiniApi/"]
RUN  dotnet tool install dotnet-ef -g    
RUN dotnet restore "./OtusMiniApi/OtusMiniApi.csproj" 
COPY . . 
WORKDIR "/src/OtusMiniApi"
RUN dotnet build "./OtusMiniApi.csproj" -c $BUILD_CONFIGURATION -o /app/build


# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OtusMiniApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OtusMiniApi.dll"]