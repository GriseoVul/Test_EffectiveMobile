using SortOrders.API;
using SortOrders.API.Models;
using SortOrders.API.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IOrderRepo, OrderRepo>();

builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddProvider(
        new FileLoggerProvider(
            builder.Configuration
                .GetSection("AppSettings")
                .Get<AppSettings>()
                .LogFile
                ));
});

var app = builder.Build();

app.Logger.LogInformation("App succesfully builded!");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "search",
    pattern: "api/{controller=Order}/{action=Search}");

app.Logger.LogInformation("Starting app..");

app.Run();
