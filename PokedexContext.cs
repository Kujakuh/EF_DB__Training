using EF_DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

public class PokedexContext : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Types> Types { get; set; }


    public string DbPath { get; }
    public dbType DbType { get; }

    public IConfiguration Config { get; set; }

    public PokedexContext(dbType target, IConfiguration config)

    {
        // Release
        //var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        // Debug
        var dbPath = Directory.GetCurrentDirectory();

        Config = config;
        DbType = target;
        DbPath = Path.Join(dbPath, "pkDex" + DbType.ToString() + ".db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*
         *  TO REVIEW : Use of external settigns file
         */
        switch (DbType)
        {
            case dbType.SQLSERVER:
                optionsBuilder.UseSqlServer(Config.GetConnectionString("local_SQLServerDB") + DbPath);
                break;

            case dbType.POSTGRE:
                optionsBuilder.UseNpgsql(Config.GetConnectionString("local_PostgreDB") + DbPath);
                break;
        }

    }
}
