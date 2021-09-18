FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Sonar.Console/Sonar.Console.csproj", "Sonar.Console/"]
RUN dotnet restore "Sonar.Console/Sonar.Console.csproj"
COPY . .
WORKDIR "/src/Sonar.Console"
RUN dotnet build "Sonar.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sonar.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sonar.Console.dll"]
