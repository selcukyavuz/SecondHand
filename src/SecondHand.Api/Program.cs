using MediatR;
using Microsoft.EntityFrameworkCore;
using SecondHand.Api.BackgroundServices;
using SecondHand.Api.Models;
using SecondHand.Library;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SecondHandDatabaseSettings>(
    builder.Configuration.GetSection("SecondHandDatabase"));
builder.Services.AddDbContextFactory<SecondHand.DataAccess.SqlServer.SecondHandContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddSingleton<SecondHand.DataAccess.SqlServer.Api.IAthleteDataAccess, SecondHand.DataAccess.SqlServer.Api.AthleteDataAccess>();
builder.Services.AddSingleton<SecondHand.DataAccess.SqlServer.Api.ITokenExchangeDataAccess, SecondHand.DataAccess.SqlServer.Api.TokenExchangeDataAccess>();
builder.Services.AddSingleton<SecondHand.DataAccess.MongoDB.Api.IAthleteDataAccess, SecondHand.DataAccess.MongoDB.Api.AthleteDataAccess>();
builder.Services.AddSingleton<SecondHand.DataAccess.MongoDB.Api.ITokenExchangeDataAccess, SecondHand.DataAccess.MongoDB.Api.TokenExchangeDataAccess>();
builder.Services.AddMediatR(typeof(SecondHandLibraryEntryPoint).Assembly);
builder.Services.AddHostedService<AthleteCreatedEventHandler>();
builder.Services.AddHostedService<AthleteEventUpdatedHandler>();
builder.Services.AddHostedService<AthleteDeletedEventHandler>();
builder.Services.AddHostedService<TokenExchangeCreatedEventHandler>();


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
