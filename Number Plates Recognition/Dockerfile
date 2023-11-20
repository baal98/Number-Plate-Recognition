#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Number Plates Recognition/Number Plates Recognition.csproj", "Number Plates Recognition/"]
RUN dotnet restore "Number Plates Recognition/Number Plates Recognition.csproj"
COPY . .
WORKDIR "/src/Number Plates Recognition"
RUN dotnet build "Number Plates Recognition.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Number Plates Recognition.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Number Plates Recognition.dll"]