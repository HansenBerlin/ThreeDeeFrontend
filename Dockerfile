FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ThreeDeeFrontend/ThreeDeeFrontend.csproj", "ThreeDeeFrontend/"]
COPY ["ThreeDeeApplication/ThreeDeeApplication.csproj", "ThreeDeeApplication/"]
COPY ["ThreeDeeInfrastructure/ThreeDeeInfrastructure.csproj", "ThreeDeeInfrastructure/"]
RUN dotnet restore "ThreeDeeFrontend/ThreeDeeFrontend.csproj"
COPY . .
RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
WORKDIR "/src/ThreeDeeFrontend"
RUN dotnet build "ThreeDeeFrontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThreeDeeFrontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
#ENV ASPNETCORE_URLS="http://+:5000"
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThreeDeeFrontend.dll"]
