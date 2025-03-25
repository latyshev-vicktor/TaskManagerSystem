using AuthenticationService.Application;
using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Infrastructure.Impl;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var senderEmail = builder.Configuration["Email:SenderEmail"];
var sender = builder.Configuration["Email:Sender"];
var host = builder.Configuration["Email:Host"];
var port = builder.Configuration.GetValue<int>("Email:Port");

builder.Services.AddPostgres(builder.Configuration)
                .AddApplication()
                .AddImplementation()
                .AddFluentEmail(senderEmail, sender)
                .AddSmtpSender(host, port);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Api", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

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
