using AuthenticationService.Api.Extensions;
using AuthenticationService.Application;
using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Infrastructure.Impl;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TaskManagerSystem.Common.Options;

var builder = WebApplication.CreateBuilder(args);

var senderEmail = builder.Configuration["Email:SenderEmail"];
var sender = builder.Configuration["Email:Sender"];
var host = builder.Configuration["Email:Host"];
var port = builder.Configuration.GetValue<int>("Email:Port");

builder.Services.AddPostgres(builder.Configuration)
                .AddApplication()
                .AddImplementation()
                .AddCustomAuthentication(builder.Configuration)
                .AddFluentEmail(senderEmail, sender)
                .AddSmtpSender(host, port);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.Configure<JwtSettings>(options => builder.Configuration.GetSection("JwtSettings").Bind(options));

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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
