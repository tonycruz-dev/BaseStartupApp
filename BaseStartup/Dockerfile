#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BaseStartup/BaseStartup.csproj", "BaseStartup/"]
RUN dotnet restore "BaseStartup/BaseStartup.csproj"
COPY . .
WORKDIR "/src/BaseStartup"
RUN dotnet build "BaseStartup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaseStartup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaseStartup.dll"]