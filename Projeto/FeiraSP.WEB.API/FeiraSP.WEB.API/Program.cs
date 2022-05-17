using FeiraSP.WEB.API.Data;
using FeiraSP.WEB.API.Services;
using FeiraSP.WEB.API.CustomErrors;
using Microsoft.EntityFrameworkCore;
using FeiraSP.WEB.API.CustomLog;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Configura o EntityFramework com a base de dados
builder.Services.AddDbContext<FeiraContext>(optBuilder =>
    optBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//Injeta a dependencia da classe de servico 
builder.Services.AddScoped<IFeiraService, FeiraService>();
builder.Services.AddScoped<IDistritoService, DistritoService>();
builder.Services.AddScoped<ISubPrefeituraService, SubPrefeituraService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Feiras na Cidade de São Paulo (Capital)",
        Description = "API RestFull desenvolvida para processo seletivo na empresa Único",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Joao Paulo S Almeida",
            Url = new Uri("https://www.linkedin.com/in/joaopaulosto/")
        },
        License = new OpenApiLicense
        {
            Name = "APACHE LICENSE, VERSION 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddSingleton<IFeiraLog, FeiraNLog>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();



    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<FeiraErrorHandler>();

app.Run();
