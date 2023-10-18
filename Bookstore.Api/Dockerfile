#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookStore.Api/Bookstore.API.csproj", "BookStore.Api/"]
COPY ["BookStore.Application/Bookstore.Application.csproj", "BookStore.Application/"]
COPY ["BookStore.DataAccess/Bookstore.DataAccess.csproj", "BookStore.DataAccess/"]
COPY ["BookStore.Domain/Bookstore.Domain.csproj", "BookStore.Domain/"]
COPY ["BookStore.Implementation/Bookstore.Implementation.csproj", "BookStore.Implementation/"]
RUN dotnet restore "BookStore.Api/Bookstore.API.csproj"
COPY . .
WORKDIR "/src/BookStore.Api"
RUN dotnet build "Bookstore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bookstore.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bookstore.API.dll"]
