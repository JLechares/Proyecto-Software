using Application.Interfaces;
using Application.UseCases.Events.Handlers;
using Domain.Entities;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//custom
builder.Services.AddScoped<IGetAllEventsQueryHandler, GetAllEventsHandler>();
builder.Services.AddScoped<IGetSectorsByEventQueryHandler, GetSectorsByEventHandler>();
builder.Services.AddScoped<IGetSeatsStatusQueryHandler, GetSeatsStatusHandler>();
builder.Services.AddScoped<IReserveSeatCommandHandler, ReserveSeatHandler>();

builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();


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
