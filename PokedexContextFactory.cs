﻿using EF_DB;
using Microsoft.EntityFrameworkCore.Design;
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

        DbType targetDb = DbType.SQLSERVER;
        Enum.TryParse(args[0], true, out targetDb);

        return new PokedexContext(targetDb, config);
    }
}
