

using Application;
using Core;
using Microsoft.Extensions.Options;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

builder.Services.AddApplicationDI();
builder.Services.AddAuthentication();
var app = builder.Build();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpMetrics(options =>
{
    options.ReduceStatusCodeCardinality();
    options.AddCustomLabel("host", context => context.Request.Host.Host);
});
app.UseMetricServer();
app.UseHttpsRedirection();
app.UseAuthentication();     
app.UseAuthorization();
app.MapControllers(); 
app.UseEndpoints(end =>
    {
        _ = end.MapMetrics(pattern: "/sergey/prometheus");
    }

    );

app.Run();
