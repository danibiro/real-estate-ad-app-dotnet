// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using bdim1996_dotnet.Controllers.Dto.Incoming;
using bdim1996_dotnet.Controllers.Mappers.Profiles;
using bdim1996_dotnet.Models.Entities;
using bdim1996_dotnet.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySQLConnection") ?? throw new Exception("Connection string not found");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<RealEstateContext>(opt => opt.UseMySql(connectionString, serverVersion));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<RealEstateAdProfile>();
    cfg.AddProfile<RealEstateAgentProfile>();
});

// Add Serilog
builder.Host.UseSerilog();

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMvc();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
