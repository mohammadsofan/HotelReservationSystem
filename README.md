# Hotel Reservation System

A modern hotel reservation system built with ASP.NET Core (.NET 8), designed with Clean Architecture and CQRS principles.  
This project provides a robust, extensible foundation for managing hotel rooms, bookings, facilities, and customer reviews with secure authentication, authorization, and high-performance caching.

---

## Features

- **Authentication & Authorization**
  - User registration and login
  - Role-based access control (Admin, Guest) using JWT
- **Room Management**
  - Full CRUD operations for rooms
  - Assign facilities (WiFi, AC, Pool, etc.)
- **Facility Management**
  - Manage hotel facilities with CRUD
- **Booking Management**
  - Create, update, cancel reservations
  - Automatic date conflict checks
- **Review System**
  - Guests can leave reviews and ratings for rooms
- **Validation & Error Handling**
  - Request validation with FluentValidation
  - Centralized exception handling middleware
- **Performance & Maintainability**
  - CQRS pattern with MediatR
  - Logging with Serilog
  - Mapster for object mapping
  - **Distributed Caching with Redis**
    - Cache-aside strategy applied to all controllers
    - Sliding & absolute expiration policies
    - Automatic cache invalidation on create/update/delete operations
- **Data Persistence**
  - Entity Framework Core with SQL Server
  - Automatic migrations and seed data on startup

---

## Tech Stack

- **Framework**: ASP.NET Core 8 (Web API)
- **Database**: SQL Server (configurable)
- **ORM**: Entity Framework Core
- **Architecture**: Clean Architecture + CQRS
- **Caching**: Redis (via Docker)
- **Libraries**:
  - MediatR (CQRS)
  - FluentValidation (validation)
  - Mapster (object mapping)
  - Serilog with Seq (logging)
- **Authentication**: JWT
- **DevOps**: Docker & Docker Compose
- **Documentation**: Swagger (OpenAPI)

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/mohammadsofan/HotelReservationSystem.git
cd HotelReservationSystem
```

### 2. Run database migrations

Migrations are applied automatically on startup via the built-in seeder.

### 3. Run with Docker

```bash
docker-compose up --build
```

- API available at: [http://localhost:5001](http://localhost:5001)
- Swagger docs: [http://localhost:5001/swagger](http://localhost:5001/swagger)
- Seq logging UI: [http://localhost:9090](http://localhost:9090)
- **Redis Commander**: [http://localhost:7001](http://localhost:7001)

---

## Project Structure

```plaintext
HotelReservationSystem/
│
├── HotelReservationSystem.Domain         # Domain entities, enums, constants
├── HotelReservationSystem.Application    # Application logic, CQRS handlers, DTOs, validation
├── HotelReservationSystem.Infrastructure # Data access, repositories, cache service, seeders
├── HotelReservationSystem.Api            # API controllers, middleware, configuration
└── docker-compose.yml                    # Docker setup (API, DB, Redis, Seq, Redis Commander)
```

---

## API Documentation

Swagger UI is enabled by default:  
[http://localhost:5001/swagger](http://localhost:5001/swagger)

---

## Next steps

- Add support for multiple hotels and branches
- Implement payment gateway integration (Stripe/PayPal)
- Add reporting module (occupancy, revenue stats)
- Implement receptionist and housekeeping roles
- Enhance review system with photos
