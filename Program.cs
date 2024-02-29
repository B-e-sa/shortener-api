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
builder.Services.AddScoped<CreateUrlService>();
builder.Services.AddScoped<DeleteUrlService>();
builder.Services.AddScoped<FindUrlByIdService>();
builder.Services.AddScoped<FindUrlByShortUrlService>();

// REPOSITORIES
builder.Services.AddScoped<IUrlRepository, UrlRepository>();

// DATABASE
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