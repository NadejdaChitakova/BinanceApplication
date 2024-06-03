using BinanceApplication.BLL;
using BinanceApplication.BLL.Services;
using BinanceApplication.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
})
.AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCore(builder.Configuration);
builder.Services.AddHostedService<StreamWorker>();
var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();


app.Services.GetRequiredService<ILogger<Program>>().LogInformation("Registered Output Formatters:");
foreach (var formatter in app.Services.GetRequiredService<IOptions<MvcOptions>>().Value.OutputFormatters)
{
    logger.LogInformation(formatter.GetType().FullName);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();
app.UseWebSockets();
app.Run();
