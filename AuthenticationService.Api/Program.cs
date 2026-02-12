using AuthenticationService.Api;
using AuthenticationService.Api.Extensions;
using AuthenticationService.Api.Handlers;
using AuthenticationService.Application;
using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Infrastructure.Impl;
using MassTransit;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Security.Principal;
using System.Text.Json.Serialization;
using TaskManagerSystem.Common.CommonMiddlewares;
using TaskManagerSystem.Common.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(options => builder.Configuration.GetSection("JwtSettings").Bind(options));

builder.Services.AddPostgres(builder.Configuration)
                .AddApplication()
                .AddImplementation()
                .AddHttpContextAccessor()
                .AddCustomAuthentication(builder.Configuration)
                .AddCustomCors()
                .AddScoped<IPrincipal>(x => x.GetService<IHttpContextAccessor>().HttpContext?.User);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                }).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                  .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    var connection = builder.Configuration.GetConnectionString("Redis") 
        ?? throw new InvalidOperationException("Строка подключения к Redis не найдена");

    options.Configuration = connection;
});

builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
{
    var connection = builder.Configuration.GetConnectionString("Redis")
        ?? throw new InvalidOperationException("Строка подключения к Redis не найдена");

    return ConnectionMultiplexer.Connect(connection);
});

builder.Services.AddSingleton<IDistributedLockProvider>(options =>
{
    var connectionMultiplexer = options.GetRequiredService<IConnectionMultiplexer>();
    return new RedisDistributedSynchronizationProvider(connectionMultiplexer.GetDatabase());
});

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
var migrations = await dbContext.Database.GetPendingMigrationsAsync();
if (migrations.Any())
{
    await dbContext.Database.MigrateAsync().ConfigureAwait(false);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseExceptionHandler();
app.UseMiddleware<TaskCancellationMiddleware>();
app.UseHttpsRedirection();

app.UseCors("AuthenticationPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
