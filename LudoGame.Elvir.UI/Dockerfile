FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LudoGame.Elvir.UI/LudoGame.Elvir.UI.csproj", "LudoGame.Elvir.UI/"]
RUN dotnet restore "LudoGame.Elvir.UI/LudoGame.Elvir.UI.csproj"
COPY . .
WORKDIR "/src/LudoGame.Elvir.UI"
RUN dotnet build "LudoGame.Elvir.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LudoGame.Elvir.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LudoGame.Elvir.UI.dll"]
