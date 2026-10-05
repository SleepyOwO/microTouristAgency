using MongoDB.Driver;
using TouristAgencyAPI.Models;
using TouristAgencyAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("TouristAgencyDb"));
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Tour>("Tours"));

/*builder.Services.Configure<TourAgencyDatabaseSettings>(builder.Configuration.GetSection("TouristAgencyDB"));
builder.Services.AddSingleton<ToursController>();
builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }