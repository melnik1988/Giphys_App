using Giphys_App.Interface;
using Giphys_App.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Services 
builder.Services.AddScoped<IHelper, HelperService>();
builder.Services.AddScoped<IFile, FileService>();
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
IConfiguration configuration = app.Configuration;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
        options => options.WithOrigins(configuration.GetSection("AppSettings:Origin").Value).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
    );

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
