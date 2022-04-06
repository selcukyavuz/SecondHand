using MediatR;
using Microsoft.EntityFrameworkCore;
using SecondHand.Api.BackgroundServices;
using SecondHand.Library;
using SecondHand.Library.DataAccess;
using SecondHand.Api.Models;
using SecondHand.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<SecondHandDatabaseSettings>(builder.Configuration.GetSection("SecondHandDatabase"));
builder.Services.AddSingleton<BooksService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<SecondHandContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!));
builder.Services.AddSingleton<IDataAccess, DataAccess>();
builder.Services.AddMediatR(typeof(SecondHandLibraryEntryPoint).Assembly);
builder.Services.AddHostedService<NewPersonEventHandler>();


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
