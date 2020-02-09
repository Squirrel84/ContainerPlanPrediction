FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["../ContainerPlanPrediction/ContainerPlanPrediction.csproj", "../ContainerPlanPrediction/"]
RUN dotnet restore "../ContainerPlanPrediction/ContainerPlanPrediction.csproj"
COPY . .
WORKDIR "/src/../ContainerPlanPrediction"
RUN dotnet build "ContainerPlanPrediction.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContainerPlanPrediction.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContainerPlanPrediction.dll"]