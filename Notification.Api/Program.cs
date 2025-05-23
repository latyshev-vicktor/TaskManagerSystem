using MassTransit;
using Notification.Api.Extension;
using Notification.Application.Consumers;
using Notification.Application.Options;
using Notification.Infrastructure.Impl;
using TaskManagerSystem.Common.CommonMiddlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure()
                .AddCustomCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SmptSetting>(options => builder.Configuration.GetSection("SmptSettings").Bind(options));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendEmailByCreatedNewUserConsumer>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TaskCancellationMiddleware>();
app.UseHttpsRedirection();

app.UseCors("NotificationPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
