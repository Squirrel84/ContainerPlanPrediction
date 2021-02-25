#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ContainerPlanPredictionApp/ContainerPlanPrediction.csproj", "ContainerPlanPredictionApp/"]
COPY ["ContainerPlanPredictionML.Model/ContainerPlanPredictionML.Model.csproj", "ContainerPlanPredictionML.Model/"]
RUN dotnet restore "ContainerPlanPredictionApp/ContainerPlanPrediction.csproj"
COPY . .
WORKDIR "/src/ContainerPlanPredictionApp"
RUN dotnet build "ContainerPlanPrediction.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContainerPlanPrediction.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContainerPlanPrediction.dll"]