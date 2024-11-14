using System.Reflection;
using Microsoft.OpenApi.Models;
using Qel.Api.Transport.Generic;
using Qel.Api.Transport.RabbitMq.Client;
using Qel.Api.Transport.RabbitMq.Models;
using Qel.Api.Transport.RabbitMq.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi (.net 9.0)
builder.Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "Gateway API", 
            Version = "v1",
            Description = "Шлюз. Предоставляет методы для отправки заявок",
            Contact = new OpenApiContact
            {
                Name = "Kirill",
                Email = string.Empty,
                Url = new Uri("https://github.com/QelBast")
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
builder.Services
    .AddSingleton<ITransportSender, Sender>()
    .Configure<SenderOptions>(builder.Configuration.GetSection(nameof(SenderOptions)));

builder.AddRabbitMqCustomClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Passport Provider API v1");
    });

    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();