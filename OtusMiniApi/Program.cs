

using Application;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataProtection()
                .UseCryptographicAlgorithms(
                       new AuthenticatedEncryptorConfiguration
                       {
                           EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                           ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                       })
               .PersistKeysToFileSystem(new DirectoryInfo(@"keyFile")) // Or other shared storage
               .SetApplicationName("SharedCookieApp");
// Add services to the container. 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

builder.Services.AddApplicationDI();
builder.Services.AddAuthorization();

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
