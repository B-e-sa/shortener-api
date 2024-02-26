using Microsoft.EntityFrameworkCore;
using Shortener.Data;
using Shortener.Repositories;
using Shortener.Repositories.Models;
using Shortener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// SERVICES
builder.Services.AddScoped<UrlService>();

// REPOSITORIES
builder.Services.AddScoped<IUrlRepository, UrlRepository>();

// DB
builder.Services.AddDbContext<ShortenerDbContext>(x =>
    {
        x.UseSqlite(builder.Configuration.GetConnectionString("Database"));
    },
    ServiceLifetime.Scoped
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
