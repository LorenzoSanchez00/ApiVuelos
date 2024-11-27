using ApiVuelos.Automappers;
using ApiVuelos.DTOs;
using ApiVuelos.Models;
using ApiVuelos.Repository;
using ApiVuelos.Services;
using ApiVuelos.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Interfaz de Servicios
builder.Services.AddKeyedScoped<ICommonService<VueloDto, CrearVueloDto, ModificarVueloDto>, VueloService>("FlyService");
builder.Services.AddKeyedScoped<ICommonService<AerolineaDto, AerolineaDto, AerolineaDto>, AerolineaService>("AerolineService");
builder.Services.AddKeyedScoped<IVueloService<VueloDto>, VueloService>("OnlyFlyService");

//Repository
builder.Services.AddScoped<IRepository<Vuelo>, VueloRepository>();
builder.Services.AddScoped<IRepository<Aerolinea>, AerolineaRepository>();
builder.Services.AddScoped<IVueloRepository<Vuelo>, VueloRepository>();

//EF
builder.Services.AddDbContext<VuelosDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

//FluentValidation
builder.Services.AddScoped<IValidator<CrearVueloDto>, CrearVuelosValidator>();
builder.Services.AddScoped<IValidator<ModificarVueloDto>,  ModificarVuelosValidator>();
builder.Services.AddScoped<IValidator<AerolineaDto>, AerolineaValidator>();

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
