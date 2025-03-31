using AuthenticationService.Api;
using AuthenticationService.Api.Extensions;
using AuthenticationService.Api.Handlers;
using AuthenticationService.Application;
using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Infrastructure.Impl;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text.Json.Serialization;
using TaskManagerSystem.Common.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(options => builder.Configuration.GetSection("JwtSettings").Bind(options));
builder.Services.Configure<SmptSettings>(options => builder.Configuration.GetSection("SmptSettings").Bind(options));

builder.Services.AddPostgres(builder.Configuration)
                .AddApplication()
                .AddImplementation()
                .AddHttpContextAccessor()
                .AddCustomAuthentication(builder.Configuration);

builder.Services.AddScoped<IPrincipal>(x => x.GetService<IHttpContextAccessor>().HttpContext?.User);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
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
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
