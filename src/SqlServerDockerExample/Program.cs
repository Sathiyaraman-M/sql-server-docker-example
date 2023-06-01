using Microsoft.EntityFrameworkCore;
using SqlServerDockerExample.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var dbHost = Environment.GetEnvironmentVariable("DBHOST") ?? "localhost";
var dbPort = Environment.GetEnvironmentVariable("DBPORT") ?? "1433";
var dbUser = Environment.GetEnvironmentVariable("DBUSER") ?? "sa";
var dbPassword = Environment.GetEnvironmentVariable("DBPASSWORD") ?? "Your_password123";
var dbConnectionString = $"Server={dbHost},{dbPort};Database=master;User Id={dbUser};Password={dbPassword};";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(dbConnectionString);
});
builder.Services.AddScoped<DepartmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
