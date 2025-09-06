# HotelReservationSystem

A modular hotel reservation management system built with **ASP.NET Core 8**, **CQRS with MediatR**, **Entity Framework Core**, **Redis caching**, and **Serilog with Seq logging**.  
The solution is structured with clean architecture principles: **Api**, **Application**, **Domain**, and **Infrastructure** layers.

---

## Features

- **User Management**: Register, login (JWT-based), update, delete users with roles (Admin, Guest).  
- **Rooms**: Create, update, delete rooms, assign/remove facilities, view room details.  
- **Bookings**: Create, view, update, delete bookings (Admin and Guest).  
- **Facilities**: Manage facilities and assign them to rooms.  
- **Reviews**: Guests can add reviews for rooms, admins can manage them.  
- **Caching**: Redis cache for users, bookings, rooms, facilities, and reviews.  
- **Logging**: Structured logging with Serilog and Seq.  
- **Error Handling**: Centralized middleware for validation, authentication, and business exceptions.

---

## Solution Structure

```
HotelReservationSystem/
├── HotelReservationSystem.Api/           # Presentation layer (controllers, middleware)
├── HotelReservationSystem.Application/   # Application layer (CQRS, DTOs, validators)
├── HotelReservationSystem.Domain/        # Domain entities and enums
├── HotelReservationSystem.Infrastructure/# Infrastructure (DbContext, repositories, services)
├── docker-compose.yml                    # Docker setup (API, SQL Server, Redis, Seq)
└── HotelReservationSystem.sln            # Visual Studio solution
```

---

## Prerequisites

- **Docker Desktop** or Docker Engine + Compose V2  
- **.NET 8 SDK** (optional if running locally without Docker)

---

## Quick Start (Docker)

1. Clone the repository.  
2. From the project root, run:

```bash
docker compose up --build
```

3. Open Swagger UI:  
   - [http://localhost:5001/swagger](http://localhost:5001/swagger)  
   - or [https://localhost:5002/swagger](https://localhost:5002/swagger)

---

## Services & Ports

| Service            | Host Port | Container Port |
|--------------------|-----------|----------------|
| API                | 5001/5002 | 8080/8081      |
| SQL Server         | 1433      | 1433           |
| Redis              | 6379      | 6379           |
| Redis Commander    | 7001      | 8081           |
| Seq (logging UI)   | 9090      | 80             |

---

## Configuration

Settings are in **appsettings.json** and environment variables in `docker-compose.yml`.

- **Connection String**:  
  ```
  Server=db;Database=HotelReservationSystem;User Id=sa;Password=####;...
  ```
- **JWT Settings**:  
  - Expiration: 60 minutes  
- **Redis**: `redis:6379`  
- **Seq**: `http://seq:80`

⚠️ *Change secrets (DB password, JWT key) before production.*

---

## API Overview

### Authentication (AccountController)
- `POST /api/Account/register` — register new user  
- `POST /api/Account/login` — login and get JWT  
- `GET /api/Account` (Admin only) — get all users  

### Rooms (RoomsController)
- `GET /api/Rooms` — list rooms  
- `GET /api/Rooms/{id}` — get room by ID  
- `POST /api/Rooms` (Admin) — create room  
- `PUT /api/Rooms/{id}` (Admin) — update room  
- `DELETE /api/Rooms/{id}` (Admin) — delete room  
- `POST /api/Rooms/{roomId}/facility/{facilityId}` — assign facility to room  
- `DELETE /api/Rooms/{roomId}/facility/{facilityId}` — remove facility  

### Bookings (BookingsController)
- `GET /api/Bookings` (Admin) — list all bookings  
- `GET /api/Bookings/current` (User) — get current user's bookings  
- `POST /api/Bookings` (User/Admin) — create booking  
- `PUT /api/Bookings/{id}` (Admin) — update booking  
- `DELETE /api/Bookings/{id}` (Admin) — delete booking  

### Facilities (FacilitiesController)
- `GET /api/Facilities` — list facilities  
- `POST /api/Facilities` (Admin) — create facility  

### Reviews (ReviewsController)
- `GET /api/Reviews` (Admin) — list reviews  
- `GET /api/Reviews/Room/{id}` — get reviews for a room  
- `POST /api/Reviews` (User/Admin) — create review  

---

## Development (Run Locally)

1. Setup SQL Server and Redis locally.  
2. Update `appsettings.json` connection strings.  
3. Run migrations:

```bash
cd HotelReservationSystem.Infrastructure
dotnet ef database update
```

4. Run the API project:

```bash
cd HotelReservationSystem.Api
dotnet run
```

---

## Logging & Monitoring

- All logs are pushed to **Seq** (`http://localhost:9090`).  
- View queries, exceptions, and structured logs.

---

## Notes & TODOs

- Implement email notifications for bookings.  
- Improve unit test coverage.  
- Move secrets to environment variables or secret manager.  

---

## License

This project is for educational/demo purposes. Replace sensitive values before production.
