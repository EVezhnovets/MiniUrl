using MiniUrl.Infrastructure;
using Serilog;
using Serilog.Events;

#region Logger
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .CreateBootstrapLogger();
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();
Dependencies.ConfigureServices(builder.Configuration, builder.Services, builder.Logging);

var app = builder.Build();

app.UseSerilogRequestLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();