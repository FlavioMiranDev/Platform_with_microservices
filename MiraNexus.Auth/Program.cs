using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiraNexus.Auth.Data;
using MiraNexus.Auth.Models;
using MiraNexus.Auth.Repositories;
using MiraNexus.Auth.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("MySQLConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQLConnection"))));

builder.Services.AddStackExchangeRedisCache(options => options.Configuration = builder.Configuration.GetConnectionString("RedisConnection"));

builder.Services.AddCors(options =>
        options.AddPolicy("AllowAll", policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        ));

builder.Services.AddMassTransit(bus =>
{
    bus.SetKebabCaseEndpointNameFormatter();

    bus.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMq:User"]);
            h.Password(builder.Configuration["RabbitMq:Pass"]);
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AuthRepository>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<PasswordService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.MapControllers();

app.Run();
