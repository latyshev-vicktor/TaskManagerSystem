using AnalyticsService.Infrastructure.Impl;
using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using AnalyticsService.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPostgres(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddCustomMassTransit()
    .AddApplication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AnalyticsDbContext>();
var migrations = await dbContext.Database.GetPendingMigrationsAsync();
if (migrations.Any())
{
    await dbContext.Database.MigrateAsync().ConfigureAwait(false);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
