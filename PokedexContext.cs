using EF_DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class PokedexContext : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Types> Types { get; set; }
    public DbSet<PokemonTypesJT> PokemonTypesJT { get; set; }

    public DbType DBType { get; }

    public IConfiguration Config { get; set; }

    public PokedexContext(DbType target, IConfiguration config)
    {
        Config = config;
        DBType = target;
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
        string dbName = $"pkDex.\"{DBType}\"";
        switch (DBType)
        {
            case DbType.SQLSERVER:
                optionsBuilder.UseSqlServer(Config.GetConnectionString("local_SQLServerDB") + dbName);
                break;

            case DbType.POSTGRE:
                optionsBuilder.UseNpgsql(Config.GetConnectionString("local_PostgreDB") + dbName);
                break;
        }

    }
}
