using FeiraSP.WEB.API.Data;
using FeiraSP.WEB.API.Services;
using FeiraSP.WEB.API.CustomErrors;
using Microsoft.EntityFrameworkCore;
using FeiraSP.WEB.API.CustomLog;

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
builder.Services.AddSwaggerGen();

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
