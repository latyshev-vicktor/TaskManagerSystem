using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Notification.Api.Extension;
using Notification.Application;
using Notification.Application.Hubs;
using Notification.Application.Options;
using Notification.DataAccess.Postgres;
using Notification.Infrastructure.Impl;
using System.Security.Principal;
using System.Text.Json.Serialization;
using TaskManagerSystem.Common.CommonMiddlewares;
using TaskManagerSystem.Common.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(options => builder.Configuration.GetSection("JwtSettings").Bind(options));

builder.Services.AddInfrastructure()
                .AddPostgres(builder.Configuration)
                .AddCustomCors()
                .AddCustomAuthentication(builder.Configuration)
                .AddCustomMassTransit()
                .AddApplication()
                .AddHttpContextAccessor()
                .AddScoped<IPrincipal>(x => x.GetService<IHttpContextAccessor>().HttpContext?.User)
                .AddSignalR();

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

builder.Services.Configure<SmptSetting>(options => builder.Configuration.GetSection("SmptSettings").Bind(options));

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
await dbContext.Database.MigrateAsync().ConfigureAwait(false);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TaskCancellationMiddleware>();
app.UseHttpsRedirection();

app.UseCors("NotificationPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/hubs/notification");
app.Run();
