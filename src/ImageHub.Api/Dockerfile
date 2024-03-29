#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ImageHub.Api/ImageHub.Api.csproj", "src/ImageHub.Api/"]
RUN dotnet restore "src/ImageHub.Api/ImageHub.Api.csproj"
COPY . .
WORKDIR "/src/src/ImageHub.Api"
RUN dotnet build "ImageHub.Api.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "ImageHub.Api.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageHub.Api.dll"]