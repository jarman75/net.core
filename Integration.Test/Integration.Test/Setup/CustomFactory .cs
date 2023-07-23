using Example.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class CustomFactory : WebApplicationFactory<Program>
{
    // Gives a fixture an opportunity to configure the application before it gets built.
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove AppDbContext
            services.RemoveDbContext<AppDbContext>();

            // Add DB context pointing to test container
            services.AddDbContext<AppDbContext>(options => { options.UseNpgsql("Host=localhost;Database=test-db;Username=postgres;Password=postgres"); });

            // Ensure schema gets created
            services.EnsureDbCreated<AppDbContext>();
        });
    }
}