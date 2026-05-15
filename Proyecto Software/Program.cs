using Application.Interfaces;
using Application.UseCases.Events.Handlers;
using Application.UseCases.Payments.Handlers;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//custom


builder.Services.AddScoped<IGetAllEventsQueryHandler, GetAllEventsHandler>();
builder.Services.AddScoped<IGetSectorsByEventQueryHandler, GetSectorsByEventHandler>();
builder.Services.AddScoped<IGetSeatsStatusQueryHandler, GetSeatsStatusHandler>();
builder.Services.AddScoped<IReserveSeatCommandHandler, ReserveSeatHandler>();
builder.Services.AddScoped<IProcessPaymentHandler, ProcessPaymentHandler>();

builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infraestructure")
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:5500")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();


app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //swagger generator
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
