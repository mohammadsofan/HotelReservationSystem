FROM mcr.Microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.Microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HotelReservationSystem.Api/HotelReservationSystem.Api.csproj","HotelReservationSystem.Api/"]
COPY ["HotelReservationSystem.Domain/HotelReservationSystem.Domain.csproj","HotelReservationSystem.Domain/"]
COPY ["HotelReservationSystem.Application/HotelReservationSystem.Application.csproj", "HotelReservationSystem.Application/"]
COPY ["HotelReservationSystem.Infrastructure/HotelReservationSystem.Infrastructure.csproj", "HotelReservationSystem.Infrastructure/"]

RUN dotnet restore "HotelReservationSystem.Api/HotelReservationSystem.Api.csproj"
COPY . .
WORKDIR "/src/HotelReservationSystem.Api"
RUN dotnet build "HotelReservationSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HotelReservationSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelReservationSystem.Api.dll"]
