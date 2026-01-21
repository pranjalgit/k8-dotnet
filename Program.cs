using Media_MS.Data;
using Media_MS.DbUp;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var conString = builder.Configuration["MySqlConnection"];
builder.Services.AddDbContext<MediaContext>(options =>
{
    options.UseMySQL(conString);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Api test by github",
        Version = "v1",
        Description = "Order Management API 2"
    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Run DB migration on startup
using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var connectionString = config["MySqlConnection"];
    DbMigrator.Run(connectionString);
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
