using System;
using SchoolManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Use an in-memory database provider so the app can run without SQL Server during testing
builder.Services.AddDbContext<SchoolDbContext>(options => options.UseInMemoryDatabase("SchoolManagement"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

// Minimal endpoint for listing schools (used by the static UI)
app.MapGet("/api/schools", async (SchoolDbContext db) =>
{
    var list = await db.Schools.OrderBy(s => s.Id).Select(s => new { s.Id, s.Name }).ToListAsync();
    return Results.Ok(list);
});

// Ensure database created and seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
    db.Database.EnsureCreated();

    if (!db.Schools.Any())
    {
        SeedDatabase(db);
    }
}

app.Run();

// Simple seeding helper
static void SeedDatabase(SchoolDbContext db)
{
    var schools = new[] {
        new School { Name = "Green Valley High", Principal = "Alice Johnson", Address = "12 Green St" },
        new School { Name = "Riverside Secondary", Principal = "Bob Martin", Address = "34 River Rd" },
        new School { Name = "Mountainview School", Principal = "Clara Lee", Address = "56 Hill Ave" },
        new School { Name = "Lakeside Academy", Principal = "Daniel Kim", Address = "78 Lake Blvd" },
        new School { Name = "Sunset High", Principal = "Eva Brown", Address = "90 Sunset Dr" },
        new School { Name = "Maple Leaf School", Principal = "Frank Green", Address = "101 Maple St" },
        new School { Name = "Oakridge High", Principal = "Grace White", Address = "202 Oak Rd" },
        new School { Name = "Pinecrest Academy", Principal = "Henry Black", Address = "303 Pine Ln" },
        new School { Name = "Cedar Grove", Principal = "Ivy Adams", Address = "404 Cedar Ct" },
        new School { Name = "Hillside High", Principal = "Jackie Chen", Address = "505 Hilltop Rd" }
    };

    foreach (var s in schools) db.Schools.Add(s);
    db.SaveChanges();

    var students = new[] {
        new Student { FullName = "Liam Smith", StudentIdentifier = "S10001", Email = "liam.smith@example.com", Phone = "0123456789", SchoolId = db.Schools.Skip(0).First().Id },
        new Student { FullName = "Emma Johnson", StudentIdentifier = "S10002", Email = "emma.johnson@example.com", Phone = "0123456790", SchoolId = db.Schools.Skip(1).First().Id },
        new Student { FullName = "Noah Williams", StudentIdentifier = "S10003", Email = "noah.williams@example.com", Phone = "0123456791", SchoolId = db.Schools.Skip(2).First().Id },
        new Student { FullName = "Olivia Brown", StudentIdentifier = "S10004", Email = "olivia.brown@example.com", Phone = "0123456792", SchoolId = db.Schools.Skip(3).First().Id },
        new Student { FullName = "William Jones", StudentIdentifier = "S10005", Email = "william.jones@example.com", Phone = "0123456793", SchoolId = db.Schools.Skip(4).First().Id },
        new Student { FullName = "Ava Garcia", StudentIdentifier = "S10006", Email = "ava.garcia@example.com", Phone = "0123456794", SchoolId = db.Schools.Skip(5).First().Id },
        new Student { FullName = "James Miller", StudentIdentifier = "S10007", Email = "james.miller@example.com", Phone = "0123456795", SchoolId = db.Schools.Skip(6).First().Id },
        new Student { FullName = "Isabella Davis", StudentIdentifier = "S10008", Email = "isabella.davis@example.com", Phone = "0123456796", SchoolId = db.Schools.Skip(7).First().Id },
        new Student { FullName = "Benjamin Martinez", StudentIdentifier = "S10009", Email = "benjamin.martinez@example.com", Phone = "0123456797", SchoolId = db.Schools.Skip(8).First().Id },
        new Student { FullName = "Sophia Hernandez", StudentIdentifier = "S10010", Email = "sophia.hernandez@example.com", Phone = "0123456798", SchoolId = db.Schools.Skip(9).First().Id },
        new Student { FullName = "Lucas Lopez", StudentIdentifier = "S10011", Email = "lucas.lopez@example.com", Phone = "0123456799", SchoolId = db.Schools.Skip(0).First().Id },
        new Student { FullName = "Mia Gonzalez", StudentIdentifier = "S10012", Email = "mia.gonzalez@example.com", Phone = "0123456800", SchoolId = db.Schools.Skip(1).First().Id },
        new Student { FullName = "Mason Wilson", StudentIdentifier = "S10013", Email = "mason.wilson@example.com", Phone = "0123456801", SchoolId = db.Schools.Skip(2).First().Id },
        new Student { FullName = "Charlotte Anderson", StudentIdentifier = "S10014", Email = "charlotte.anderson@example.com", Phone = "0123456802", SchoolId = db.Schools.Skip(3).First().Id },
        new Student { FullName = "Ethan Thomas", StudentIdentifier = "S10015", Email = "ethan.thomas@example.com", Phone = "0123456803", SchoolId = db.Schools.Skip(4).First().Id },
        new Student { FullName = "Amelia Taylor", StudentIdentifier = "S10016", Email = "amelia.taylor@example.com", Phone = "0123456804", SchoolId = db.Schools.Skip(5).First().Id },
        new Student { FullName = "Alexander Moore", StudentIdentifier = "S10017", Email = "alex.moore@example.com", Phone = "0123456805", SchoolId = db.Schools.Skip(6).First().Id },
        new Student { FullName = "Harper Jackson", StudentIdentifier = "S10018", Email = "harper.jackson@example.com", Phone = "0123456806", SchoolId = db.Schools.Skip(7).First().Id },
        new Student { FullName = "Michael Martin", StudentIdentifier = "S10019", Email = "michael.martin@example.com", Phone = "0123456807", SchoolId = db.Schools.Skip(8).First().Id },
        new Student { FullName = "Evelyn Lee", StudentIdentifier = "S10020", Email = "evelyn.lee@example.com", Phone = "0123456808", SchoolId = db.Schools.Skip(9).First().Id }
    };

    foreach (var st in students) db.Students.Add(st);
    db.SaveChanges();
}
