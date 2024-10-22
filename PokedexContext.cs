using EF_DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class PokedexContext : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Types> Types { get; set; }
    public DbSet<PokemonTypesJT> PokemonTypesJT { get; set; }

    public dbType DbType { get; }

    public IConfiguration Config { get; set; }

    public PokedexContext(dbType target, IConfiguration config)
    {
        Config = config;
        DbType = target;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pokemon>()
            .HasMany(e => e.Types)
            .WithMany()
            .UsingEntity<PokemonTypesJT>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*
         *  TO REVIEW : Use of external settigns file
         */
        string dbName = "DB\\pkDex" + DbType.ToString() + ".db";
        switch (DbType)
        {
            case dbType.SQLSERVER:
                optionsBuilder.UseSqlServer(Config.GetConnectionString("local_SQLServerDB") + dbName);
                break;

            case dbType.POSTGRE:
                optionsBuilder.UseNpgsql(Config.GetConnectionString("local_PostgreDB") + dbName);
                break;
        }

    }
}
