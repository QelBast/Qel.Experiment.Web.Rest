using Qel.Ef.DbClient.Extensions;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Qel.Ef.Contexts.MainContext;
using Qel.Ef.Providers.Postgres;
using Qel.Ef.DbClient;
using Qel.Ef.Contexts.BlacklistContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi (.net 9.0)
builder.Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "Blacklist API", 
            Version = "v1",
            Description = "Предоставляет методы для работы с чёрным списком заявителей",
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

builder.Services.AddDbClient<DbContextBlacklist>(register =>
    {
        register.AddTransientRepository<IBlacklistRepository, BlacklistRepository<DbContextBlacklist>>();
    },
    builder.Configuration, 
    [new Configurator(nameof(DbContextBlacklist))]);
builder.Services.AddOptions();
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
