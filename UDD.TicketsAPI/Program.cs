using Microsoft.EntityFrameworkCore;
using UDD.TicketsAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Console.WriteLine($"CONEXIÓN EXITOSA: {connectionString}");

//Ahora debemos registrar el AppDbContext en el archivo Program.cs para que la aplicación sepa cómo conectarse a la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.UseSwagger();
    //app.UseSwaggerUI(); // <-- Asegúrate de tener esta línea para ver perubas con Swagger UI en el navegador
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
