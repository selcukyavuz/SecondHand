using MediatR;
using Microsoft.EntityFrameworkCore;
using SecondHand.Api.BackgroundServices;
using SecondHand.DataAccess.SqlServer;
using SecondHand.Api.Models;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SecondHandDatabaseSettings>(
    builder.Configuration.GetSection("SecondHandDatabase"));
builder.Services.AddDbContextFactory<SecondHandContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddSingleton<IAthleteDataAccess, AthleteDataAccess>();
builder.Services.AddSingleton<ITokenExchangeDataAccess, TokenExchangeDataAccess>();
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
