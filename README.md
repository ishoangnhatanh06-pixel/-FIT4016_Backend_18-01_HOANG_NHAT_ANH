# SchoolManagement (FIT4016-KiemTra-2026)

This project is a simple ASP.NET Core Web API application that manages Schools and Students using Entity Framework Core.

Features implemented:
- EF Core models for `School` and `Student` with constraints (primary keys, foreign keys, uniqueness, required fields)
- `SchoolDbContext` with configuration and automatic timestamps
- Seed data: 10 schools and 20 students
- CRUD API for Students (Create, Read with pagination, Update, Delete)
- User-friendly error messages in English

How to run

1. Ensure you have .NET SDK installed (6.0+ or later).
2. Open a terminal in this folder and run:

```bash
dotnet restore
dotnet run --project SchoolManagement
```

3. The API uses an in-memory database by default (no external database required). Data is persisted only in memory while the app runs.

API endpoints (base URL: http://localhost:5000 or printed by the app)
- `GET /api/students?page=1` - list students (10 per page)
- `GET /api/students/{id}` - get student by id
- `POST /api/students` - create student (JSON body)
- `PUT /api/students/{id}` - update student
- `DELETE /api/students/{id}` - delete student

Validation rules are enforced and return English messages.
