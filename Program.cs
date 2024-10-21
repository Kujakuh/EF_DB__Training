using EF_DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

class Program
{

    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
                        .SetBasePath("D:\\Storage\\Proyects\\C#\\EF_DB\\EF_DB__Training")
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

        Pokemon[] sourceRecordsPokemon = [];
        Types[] sourceRecordsTypes = [];

        using (PokedexContext db = new(dbType.SQLSERVER, config))
        {
            //AddSampleData(db);

            sourceRecordsPokemon = db.Pokemons.AsNoTracking().ToArray();
            sourceRecordsTypes = db.Types.AsNoTracking().ToArray();

            DropRecords<Pokemon>(db, db.DbType);
            DropRecords<Types>(db, db.DbType);
        }

        using (PokedexContext db = new(dbType.POSTGRE, config))
        {
            AddRecordsIgnoreKey<Pokemon>(db, sourceRecordsPokemon);
            AddRecordsIgnoreKey<Types>(db, sourceRecordsTypes);
        }
    }


    static void DropRecords<T>(DbContext db, dbType serverType) where T : class
    {
        var table = db.Set<T>();
        if (!table.Any()) return;

        table.RemoveRange(table);
        db.SaveChanges();

        ResetIdentity<T>(db, serverType);
    }

    static void AddRecordsIgnoreKey<T>(DbContext db,T[] records) where T : class
    {
        var entityType = db.Model.FindEntityType(typeof(T));
        var key = entityType.FindPrimaryKey().Properties.FirstOrDefault();

        if (key == null) return;

        foreach (var item in records)
        {
            var entry = db.Entry(item);

            // Key previous value is disposable
            // Let db generate a new value for the field
            entry.Property(key.Name).IsTemporary = true;
            // Gen insert command
            entry.State = EntityState.Added;
        }
        db.SaveChanges();

    }

    static void ResetIdentity<T>(DbContext db, dbType serverType) where T : class
    {
        var entityType = db.Model.FindEntityType(typeof(T));
        var tableName = entityType.GetTableName();


        switch (serverType)
        {
            case dbType.POSTGRE:
                var pgSchema = entityType.GetSchema() ?? "public";
                db.Database.ExecuteSqlRaw($"TRUNCATE TABLE \"{pgSchema}\".\"{tableName}\" RESTART IDENTITY;");
                break;
            case dbType.SQLSERVER:
                var sqlsSchema = entityType.GetSchema() ?? "dbo";
                db.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('{sqlsSchema}.{tableName}', RESEED, 0);");
                break;
        }

    }


    static public void AddSampleData(PokedexContext db)
    {

        Pokemon[] pokemons = [
            new Pokemon("Starly", "Normal", "Volador", 0.8f, 4.2f, "Un Pokémon pequeño y veloz que vuela en grandes bandadas. Su canto es débil, pero trabaja en equipo con otros Starly."),
            new Pokemon("Pikachu", "Eléctrico", "", 0.4f, 6.0f, "Famoso por sus potentes ataques eléctricos, Pikachu almacena electricidad en sus mejillas. Es muy leal a su entrenador."),
            new Pokemon("Charmander", "Fuego", "", 0.6f, 8.5f, "Su cola siempre tiene una llama que refleja su estado emocional. Es amigable pero puede ser feroz en combate."),
            new Pokemon("Bulbasaur", "Planta", "Veneno", 0.7f, 6.9f, "Lleva una semilla en su espalda que crece junto a él. Es muy dócil y utiliza sus habilidades de planta para luchar."),
            new Pokemon("Squirtle", "Agua", "", 0.5f, 9.0f, "Su caparazón lo protege de ataques y le ayuda a nadar a gran velocidad. Dispara agua a presión por su boca."),
            new Pokemon("Pidgey", "Normal", "Volador", 0.3f, 1.8f, "Un Pokémon muy común que se encuentra en muchas regiones. A pesar de su pequeño tamaño, es ágil en el aire."),
            new Pokemon("Eevee", "Normal", "", 0.3f, 6.5f, "Un Pokémon con un potencial evolutivo increíble. Tiene un carácter curioso y se adapta fácilmente a diferentes entornos."),
            new Pokemon("Machop", "Lucha", "", 0.8f, 19.5f, "Este Pokémon entrena constantemente sus músculos para volverse más fuerte. Tiene una gran energía y espíritu de lucha."),
            new Pokemon("Gastly", "Fantasma", "Veneno", 1.3f, 0.1f, "Un Pokémon formado principalmente de gas. Puede atravesar paredes y aterrorizar a sus oponentes con su presencia."),
            new Pokemon("Abra", "Psíquico", "", 0.9f, 19.5f, "Abra pasa la mayor parte de su tiempo durmiendo, pero es capaz de teletransportarse en un instante si se siente amenazado."),
            new Pokemon("Onix", "Roca", "Tierra", 8.8f, 210.0f, "Una enorme serpiente de roca que puede atravesar montañas con facilidad. Es extremadamente resistente."),
            new Pokemon("Geodude", "Roca", "Tierra", 0.4f, 20.0f, "Geodude se camufla como una roca para pasar desapercibido. Su cuerpo es muy duro y resistente a los golpes."),
            new Pokemon("Magikarp", "Agua", "", 0.9f, 10.0f, "Un Pokémon débil que no puede hacer mucho más que salpicar. Sin embargo, se rumorea que puede evolucionar en algo poderoso."),
            new Pokemon("Jigglypuff", "Normal", "Hada", 0.5f, 5.5f, "Utiliza su canto para dormir a sus oponentes. Es un Pokémon adorable pero muy efectivo en batalla con su voz hipnótica."),
            new Pokemon("Snorlax", "Normal", "", 2.1f, 460.0f, "Snorlax pasa la mayor parte de su tiempo durmiendo. Solo se despierta para comer grandes cantidades de comida."),
            new Pokemon("Dragonite", "Dragón", "Volador", 2.2f, 210.0f, "Un Pokémon muy poderoso que es capaz de volar grandes distancias en poco tiempo. Tiene un carácter noble y protector."),
            new Pokemon("Gengar", "Fantasma", "Veneno", 1.5f, 40.5f, "Gengar es muy travieso y disfruta asustando a otros Pokémon y personas. Se mueve en la oscuridad sin ser detectado."),
            new Pokemon("Lapras", "Agua", "Hielo", 2.5f, 220.0f, "Un Pokémon pacífico que suele transportar a personas sobre su espalda. Tiene una gran inteligencia y es muy protector."),
            new Pokemon("Psyduck", "Agua", "", 0.8f, 19.6f, "A menudo parece confundido, pero cuando tiene dolores de cabeza, desata poderosas habilidades psíquicas sin control."),
            new Pokemon("Cubone", "Tierra", "", 0.4f, 6.5f, "Siempre lleva el cráneo de su madre fallecida, lo que le da una apariencia triste. Es muy solitario y busca venganza por su pérdida.")
        ];

        Types[] types =
        [
            new Types { Name = "Normal", Description = "Pokémon sin afinidad elemental." },
            new Types { Name = "Fuego", Description = "Pokémon que dominan el fuego y el calor." },
            new Types { Name = "Agua", Description = "Pokémon que controlan el agua y son excelentes nadadores." },
            new Types { Name = "Planta", Description = "Pokémon que utilizan la naturaleza y las plantas para combatir." },
            new Types { Name = "Eléctrico", Description = "Pokémon que generan electricidad para atacar a sus oponentes." },
            new Types { Name = "Hielo", Description = "Pokémon que manipulan el hielo y las bajas temperaturas." },
            new Types { Name = "Lucha", Description = "Pokémon expertos en combate cuerpo a cuerpo y fuerza física." },
            new Types { Name = "Veneno", Description = "Pokémon que utilizan toxinas y venenos para debilitar a sus enemigos." },
            new Types { Name = "Tierra", Description = "Pokémon que manipulan la tierra y los suelos." },
            new Types { Name = "Volador", Description = "Pokémon que vuelan y atacan desde el cielo." },
            new Types { Name = "Psíquico", Description = "Pokémon que usan habilidades mentales y poderes psíquicos." },
            new Types { Name = "Bicho", Description = "Pokémon de tipo insecto, a menudo rápidos y resistentes." },
            new Types { Name = "Roca", Description = "Pokémon que tienen cuerpos duros y utilizan rocas para atacar." },
            new Types { Name = "Fantasma", Description = "Pokémon espectrales que pueden atravesar objetos y asustar a sus oponentes." },
            new Types { Name = "Dragón", Description = "Pokémon poderosos con habilidades de dragón." },
            new Types { Name = "Siniestro", Description = "Pokémon que usan tácticas oscuras y engañosas para atacar." },
            new Types { Name = "Acero", Description = "Pokémon con cuerpos metálicos que ofrecen gran defensa." },
            new Types { Name = "Hada", Description = "Pokémon mágicos con habilidades encantadoras y poderosas." }
        ];


        foreach (var item in types) db.Types.Add(item);
        foreach (var item in pokemons) db.Pokemons.Add(item);


        db.SaveChanges();

    }

}