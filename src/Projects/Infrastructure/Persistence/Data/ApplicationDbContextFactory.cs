using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using DotNetEnv;

namespace KanbanFlow.Projects.Infrastructure.Persistence.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Buscamos la raíz del repositorio (donde est�� la carpeta .git)
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (dir != null && !dir.GetDirectories(".git").Any())
        {
            dir = dir.Parent;
        }
        
        var basePath = dir?.FullName ?? throw new InvalidOperationException("Could not find the git repository root.");

        var dotenvPath = Path.Combine(basePath, ".env");
        if (!File.Exists(dotenvPath))
        {
            throw new InvalidOperationException($".env file not found at {dotenvPath}");
        }
        
        Env.Load(dotenvPath);

        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("The connection string 'DefaultConnection' was not found in the loaded environment variables.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}