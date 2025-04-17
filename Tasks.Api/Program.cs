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

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
