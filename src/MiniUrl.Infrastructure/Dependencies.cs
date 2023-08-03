using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiniUrl.ApplicationCore.Interfaces;

namespace MiniUrl.Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services, ILoggingBuilder loggingBuilder)
        {
            services.AddDbContext<MiniUrlContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("miniUrlConnection")));

            services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        }
    }
}