using EF_DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
                        .SetBasePath("D:\\Repos\\EF_DB")
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

        Pokemon[] sourceRecordsPokemon = [];
        Types[] sourceRecordsTypes = [];
        PokemonTypesJT[] sourceRecordsJT = [];


        using (PokedexContext db = new(dbType.SQLSERVER, config))
        {
            //DBManagementUtils.AddSampleData(db);

            sourceRecordsPokemon = db.Pokemons.AsNoTracking().ToArray();
            sourceRecordsTypes = db.Types.AsNoTracking().ToArray();
            sourceRecordsJT = db.PokemonTypesJT.AsNoTracking().ToArray();

            DBManagementUtils.DropRecords<Pokemon>(db, db.DbType);
            DBManagementUtils.DropRecords<Types>(db, db.DbType);
            DBManagementUtils.DropRecords<PokemonTypesJT>(db, db.DbType);
        }

        using (PokedexContext db = new(dbType.POSTGRE, config))
        {
            //DBManagementUtils.DropRecords<Pokemon>(db, db.DbType);
            //DBManagementUtils.DropRecords<Types>(db, db.DbType);
            //DBManagementUtils.DropRecords<PokemonTypesJT>(db, db.DbType);

            DBManagementUtils.AddRecordsIgnoreKey<Pokemon>(db, sourceRecordsPokemon);
            DBManagementUtils.AddRecordsIgnoreKey<Types>(db, sourceRecordsTypes);
            DBManagementUtils.AddRecordsIgnoreKey<PokemonTypesJT>(db, sourceRecordsJT);
        }
    }
}