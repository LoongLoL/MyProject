#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/pressreleases/NewsPublish.Web/NewsPublish.Web.csproj", "src/pressreleases/NewsPublish.Web/"]
COPY ["src/PressReleases/NewsPublish.Models/NewsPublish.Models.csproj", "src/PressReleases/NewsPublish.Models/"]
COPY ["src/pressreleases/NewsPublish.Service/NewsPublish.Service.csproj", "src/pressreleases/NewsPublish.Service/"]
RUN dotnet restore "src/pressreleases/NewsPublish.Web/NewsPublish.Web.csproj"
COPY . .
WORKDIR "/src/src/pressreleases/NewsPublish.Web"
RUN dotnet build "NewsPublish.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsPublish.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsPublish.Web.dll"]