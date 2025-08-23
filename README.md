# Hotel Reservation System

A modern hotel reservation system built with ASP.NET Core (.NET 8), designed with Clean Architecture and CQRS principles.  
This project provides a robust, extensible foundation for managing hotel rooms, bookings, facilities, and customer reviews with secure authentication and authorization.

---

## Features

- Authentication & Authorization
  - User registration and login
  - Role-based access control (Admin, Guest) using JWT
- Room Management
  - Full CRUD operations for rooms
  - Assign facilities (WiFi, AC, Pool, etc.)
- Facility Management
  - Manage hotel facilities with CRUD
- Booking Management
  - Create, update, cancel reservations
  - Automatic date conflict checks
- Review System
  - Guests can leave reviews and ratings for rooms
- Validation & Error Handling
  - Request validation with FluentValidation
  - Centralized exception handling middleware
- Performance & Maintainability
  - CQRS pattern with MediatR
  - Logging with Serilog
  - Mapster for object mapping
- Data Persistence
  - Entity Framework Core with SQL Server
  - Automatic migrations and seed data on startup

---

## Tech Stack

- Framework: ASP.NET Core 8 (Web API)
- Database: SQL Server (configurable)
- ORM: Entity Framework Core
- Architecture: Clean Architecture + CQRS
- Libraries: 
  - MediatR (CQRS)
  - FluentValidation (validation)
  - Mapster (object mapping)
  - Serilog with seq (logging)
- Authentication: JWT
- DevOps: Docker & Docker Compose
- Documentation: Swagger (OpenAPI)

---

## Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/yourusername/HotelReservationSystem.git
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
- Seq logging (default): [http://seq:5341](http://seq:5341)  

---

## Project Structure

```plaintext
HotelReservationSystem/
│
├── HotelReservationSystem.Domain         # Domain entities, enums, constants
├── HotelReservationSystem.Application    # Application logic, CQRS handlers, DTOs, validation
├── HotelReservationSystem.Infrastructure # Data access, repositories, seeders, external services
├── HotelReservationSystem.Api            # API controllers, middleware, configuration
└── docker-compose.yml                    # Docker setup (API, DB, Seq)
```

---

## API Documentation

Swagger UI is enabled by default:  
[http://localhost:5001/swagger](http://localhost:5000/swagger)

---

## Next step

- Add support for multiple hotels and branches  
- Implement payment gateway integration (Stripe/PayPal)  
- Add reporting module (occupancy, revenue stats)  
- Implement receptionist and housekeeping roles  
- Enhance review system with photos  

---
