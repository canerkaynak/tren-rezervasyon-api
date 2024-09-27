FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TrainReservationApi/TrainReservationApi.csproj", "TrainReservationApi/"]
RUN dotnet restore "TrainReservationApi/TrainReservationApi.csproj"
COPY . .
WORKDIR "/src/TrainReservationApi"
RUN dotnet build "TrainReservationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrainReservationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrainReservationApi.dll"]