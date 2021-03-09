#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarsUnlimited.WebUI/Server/CarsUnlimited.WebUI.Server.csproj", "CarsUnlimited.WebUI/Server/"]
COPY ["CarsUnlimited.WebUI/Client/CarsUnlimited.WebUI.Client.csproj", "CarsUnlimited.WebUI/Client/"]
COPY ["CarsUnlimited.WebUI/Shared/CarsUnlimited.WebUI.Shared.csproj", "CarsUnlimited.WebUI/Shared/"]
RUN dotnet restore "CarsUnlimited.WebUI/Server/CarsUnlimited.WebUI.Server.csproj"
COPY . .
WORKDIR "/src/CarsUnlimited.WebUI/Server"
RUN dotnet build "CarsUnlimited.WebUI.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarsUnlimited.WebUI.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarsUnlimited.WebUI.Server.dll"]