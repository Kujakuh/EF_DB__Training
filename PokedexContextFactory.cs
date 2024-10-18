using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class PokedexContextFactory : IDesignTimeDbContextFactory<PokedexContext>
{
    public PokedexContext CreateDbContext(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

        dbType targetDb = dbType.SQLSERVER;
        switch (args[0])
        {
            case "postgre":
                targetDb = dbType.POSTGRE;
                break;
            case "sqlserver":
                break;
        }

        return new PokedexContext(targetDb, config);
    }
}
