# Sistem Peminjaman Ruangan Kampus - Backend

Backend API untuk sistem peminjaman ruangan kampus (PBL 2026).

## Features
- Create booking
- Read booking
- Update booking
- Delete booking
- Booking status
- Basic validation

## Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger

## Cara Menjalankan
1. Clone repo
2. dotnet restore
3. dotnet run
4. Buka swagger: /swagger

## API Endpoint
POST /api/Bookings  
GET /api/Bookings  
GET /api/Bookings/{id}  
PUT /api/Bookings/{id}  
DELETE /api/Bookings/{id}