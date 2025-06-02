using Newtonsoft.Json;
using Tasks.DataAccess.Postgres;
using Tasks.Application;
using System.Text.Json.Serialization;
using Tasks.Api.Extensions;
using System.Security.Principal;
using TaskManagerSystem.Common.Options;
using Tasks.Api.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using TaskManagerSystem.Common.CommonMiddlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(options => builder.Configuration.GetSection("JwtSettings").Bind(options));

builder.Services.AddPostgres(builder.Configuration)
                .AddApplication()
                .AddCustomAuthentication(builder.Configuration)
                .AddHttpContextAccessor()
                .AddCustomCors();

builder.Services.AddScoped<IPrincipal>(x => x.GetService<IHttpContextAccessor>().HttpContext?.User);
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

builder.Services.AddStackExchangeRedisCache(options =>
{
    var connection = builder.Configuration.GetConnectionString("Redis")
        ?? throw new InvalidOperationException("Строка подключения к Redis не найдена");

    options.Configuration = connection;
});

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
await dbContext.Database.MigrateAsync().ConfigureAwait(false);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseMiddleware<TaskCancellationMiddleware>();
app.UseHttpsRedirection();

app.UseCors("TaskPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
