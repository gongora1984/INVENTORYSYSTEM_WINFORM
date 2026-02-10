using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Infrastructure.Data;
using InventorySystem.WinForms.Services;

namespace InventorySystem.WinForms;

static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

    [STAThread]
    static void Main()
    {
        Application.ThreadException += (s, e) => ShowError(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (s, e) => ShowError(e.ExceptionObject as Exception);

        try
        {
            ApplicationConfiguration.Initialize();

            var baseDir = AppContext.BaseDirectory;
            
            var host = Host.CreateDefaultBuilder()
                .UseContentRoot(baseDir)
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        var configPath = Path.Combine(baseDir, "appsettings.json");
                        bool exists = File.Exists(configPath);
                        throw new Exception($"Connection string 'DefaultConnection' not found. AppSettings exist at {configPath}: {exists}");
                    }

                    // Automatic Decryption: If it doesn't start with "Server=", try to decrypt it.
                    if (!connectionString.Trim().StartsWith("Server=", StringComparison.OrdinalIgnoreCase))
                    {
                        try {
                            connectionString = SecurityService.Decrypt(connectionString);
                        } catch { /* If decryption fails, use as-is (might be a different format) */ }
                    }

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString));

                    services.AddScoped<IInventoryService, InventoryService>();
                    services.AddTransient<MainForm>();
                })
                .Build();

            ServiceProvider = host.Services;

            // Ensure database is created
            using (var scope = ServiceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private static void ShowError(Exception? ex)
    {
        string message = ex?.ToString() ?? "Unknown error occurred.";
        MessageBox.Show(message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}