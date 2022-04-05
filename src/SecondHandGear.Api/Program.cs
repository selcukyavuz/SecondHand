using MediatR;
using Microsoft.EntityFrameworkCore;
using SecondHandGear.Library;
using SecondHandGear.Library.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<SecondHandGearContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!));
builder.Services.AddSingleton<IDataAccess, DataAccess>();
builder.Services.AddMediatR(typeof(SecondHandGearLibraryEntryPoint).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
