using Qel.Ef.Contexts.MainContext;
using Qel.Ef.DbClient;
using Qel.Ef.DbClient.Extensions;
using Qel.Ef.Providers.Postgres;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Qel.Common.Console.Hosting.RabbitMq;
using Qel.Api.Transport.Behaviours;
using Qel.Ef.Contexts.PassportContext;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi (.net 9.0)
builder.Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "Passport Provider API", 
            Version = "v1",
            Description = "Предоставляет возможность работы с паспортными данными и данными о заявителях",
            Contact = new OpenApiContact
            {
                Name = "Kirill",
                Email = string.Empty,
                Url = new Uri("https://github.com/QelBast")
            }
        });

        c.CustomSchemaIds(type => type.FullName);

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

builder.Services.AddDbClient<DbContextPassport>(register =>
    {
        register.AddTransientRepository<IPassportRepository, PassportRepository<DbContextPassport>>();
        register.AddTransientRepository<IPersonRepository, PersonRepository<DbContextPassport>>();
    },
    builder.Configuration, 
    [new Configurator(nameof(DbContextPassport))]);

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
