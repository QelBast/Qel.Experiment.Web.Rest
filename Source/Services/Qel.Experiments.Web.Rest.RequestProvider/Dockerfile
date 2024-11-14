FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Experiments/Web/Rest/Source/Services/Qel.Experiments.Web.Rest.RequestProvider/Qel.Experiments.Web.Rest.RequestProvider.csproj", "Experiments/Web/Rest/Source/Services/Qel.Experiments.Web.Rest.RequestProvider/"]
RUN dotnet restore "Experiments/Web/Rest/Source/Services/Qel.Experiments.Web.Rest.RequestProvider/Qel.Experiments.Web.Rest.RequestProvider.csproj"
COPY . .
WORKDIR "/src/Experiments/Web/Rest/Source/Services/Qel.Experiments.Web.Rest.RequestProvider"
RUN dotnet build "Qel.Experiments.Web.Rest.RequestProvider.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Qel.Experiments.Web.Rest.RequestProvider.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Qel.Experiments.Web.Rest.RequestProvider.dll"]
