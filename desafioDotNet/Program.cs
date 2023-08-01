using desafioDotNet.Context;
using Microsoft.EntityFrameworkCore;
using desafioDotNet.Models;
using desafioDotNet.Utils;
using desafioDotNet.Repository.Interfaces;
using desafioDotNet.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<NormalizeFile>();

builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

builder.Services.AddDbContext<RegisterContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "SmartOnlineDesafio",
        Version = "v1",
        Contact = new OpenApiContact {
            Name = "Yago Goncalves",
            Email = "yago.mdk284@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/yago-goncalves-da-silva/")
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();