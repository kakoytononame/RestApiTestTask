FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RestApiTestTask/RestApiTestTask.csproj", "RestApiTestTask/"]
RUN dotnet restore "RestApiTestTask/RestApiTestTask.csproj"
COPY . .
WORKDIR "/src/RestApiTestTask"
RUN dotnet build "RestApiTestTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestApiTestTask.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestApiTestTask.dll"]
