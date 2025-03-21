using Microsoft.EntityFrameworkCore;
using PruebaTecnica.BLL.Classes;
using PruebaTecnica.BLL.Interfaces;
using PruebaTecnica.DAL.DbContext;
using PruebaTecnica.DAL.Models;
using PruebaTecnica.DAL.Repositories;
using PruebaTecnica.Utility;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IRestApiService, RestApiService>(c =>
{
    c.BaseAddress = new Uri("https://api.restful-api.dev");
});

builder.Services.AddHttpClient<IRandomUserService, RandomUserService>(client =>
{
    client.BaseAddress = new Uri("https://randomuser.me/");
});

builder.Services.AddHttpClient("WebhookClient", client =>
{
    client.BaseAddress = new Uri("https://webhook.link/3cc621a3-35d9-4d30-aa8b-5b6399a285fd");
    client.DefaultRequestHeaders.Add("Surtechnology", "6E3F37EF-2DBC-4062-B974-5812DCB0B2AC");
});

builder.Services.Configure<Webhook>(builder.Configuration.GetSection("Webhook"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

builder.Services.AddScoped<IRepository<ApiLog>, Repository<ApiLog>>();
builder.Services.AddScoped<IApiLogRepository, ApiLogRepository>();

builder.Services.AddScoped<ILogApiService, LogApiService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
