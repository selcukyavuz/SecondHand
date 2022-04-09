using MediatR;
using Microsoft.EntityFrameworkCore;
using SecondHand.Api.BackgroundServices;
using SecondHand.Library;
using SecondHand.Library.DataAccess;
using SecondHand.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SecondHandDatabaseSettings>(builder.Configuration.GetSection("SecondHandDatabase"));
builder.Services.AddDbContextFactory<SecondHandContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!));
builder.Services.AddSingleton<IPersonDataAccess, PersonDataAccess>();
builder.Services.AddSingleton<IDetailedAthleteDataAccess, DetailedAthleteDataAccess>();
builder.Services.AddSingleton<ITokenExchangeDataAccess, TokenExchangeDataAccess>();
builder.Services.AddMediatR(typeof(SecondHandLibraryEntryPoint).Assembly);
builder.Services.AddHostedService<NewPersonEventHandler>();
builder.Services.AddHostedService<NewDetailedAthleteEventHandler>();
builder.Services.AddHostedService<NewTokenExchangeEventHandler>();
builder.Services.AddHostedService<UpdateDetailedAthleteEventHandler>();
builder.Services.AddHostedService<DeleteDetailedAthleteEventHandler>();


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
