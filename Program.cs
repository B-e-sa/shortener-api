using Microsoft.EntityFrameworkCore;
using Shortener.Data;
using Shortener.Repositories;
using Shortener.Repositories.Models;
using Shortener.Services.Implementations;
using Shortener.Services.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressMapClientErrors = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SERVICES
builder.Services.AddScoped<ICreateUrlService, CreateUrlService>();
builder.Services.AddScoped<IDeleteUrlService, DeleteUrlService>();
builder.Services.AddScoped<IFindUrlByIdService, FindUrlByIdService>();
builder.Services.AddScoped<IFindUrlByShortUrlService, FindUrlByShortUrlService>();

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