using Newtonsoft.Json;
using Tasks.DataAccess.Postgres;
using Tasks.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPostgres(builder.Configuration)
                .AddApplication();

builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
