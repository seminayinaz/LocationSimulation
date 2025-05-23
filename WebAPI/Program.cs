using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;
using WebAPI;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Entities.Concrete.Token>();
builder.Services.AddScoped<DataAccess.Concrete.AuthenticationManager>();
builder.Services.AddScoped<DataAccess.Concrete.CoordinateManager>();
builder.Services.AddScoped<DataAccess.Concrete.SocketManager>();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

    // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("https://localhost:7012/swagger/index.html").AllowAnyOrigin().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

