using Domain.Entities;
using Infraestructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//custom
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();


//custom
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // Crea la base de datos y las tablas basadas en tu OnModelCreating.
        context.Database.EnsureCreated();

        // Precarga inicial (1 Evento, 2 Sectores, 50 butacas por sector).
        if (!context.EVENTS.Any())
        {
            var evento = new EVENT
            {
                Name = "Piratas del caribe",
                EventDate = DateTime.Now.AddDays(30),
                Venue = "Sala 1",
                Status = "Active" // El campo requerido para evitar errores.
            };

            for (int s = 1; s <= 2; s++)
            {
                var sector = new SECTOR
                {
                    Name = $"Sector {s}",
                    Price = 5000 * s,
                    Capacity = 50
                };

                for (int b = 1; b <= 50; b++)
                {
                    sector.Seats.Add(new SEAT
                    {
                        Id = Guid.NewGuid(), // UUID como pide tu esquema.
                        RowIdentifier = "A",
                        SeatNumber = b,
                        Status = "Available",
                        Version = 1
                    });
                }
                evento.Sectors.Add(sector);
            }

            context.EVENTS.Add(evento);
            context.SaveChanges();
            Console.WriteLine("--> Base de datos generada y precargada con éxito.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al iniciar la base de datos: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //swagger generator
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
