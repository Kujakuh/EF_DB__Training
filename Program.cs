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


        using (PokedexContext db = new(DbType.POSTGRE, config))
        {
            //AddSampleData(db);

            sourceRecordsPokemon = db.Pokemons.AsNoTracking().ToArray();
            sourceRecordsTypes = db.Types.AsNoTracking().ToArray();
            sourceRecordsJT = db.PokemonTypesJT.AsNoTracking().ToArray();

            DBManagementUtils.DropRecords<Pokemon>(db, db.DBType);
            DBManagementUtils.DropRecords<Types>(db, db.DBType);
            DBManagementUtils.DropRecords<PokemonTypesJT>(db, db.DBType);
        }

        using (PokedexContext db = new(DbType.SQLSERVER, config))
        {
            //DBManagementUtils.DropRecords<Pokemon>(db, db.DBType);
            //DBManagementUtils.DropRecords<Types>(db, db.DBType);
            //DBManagementUtils.DropRecords<PokemonTypesJT>(db, db.DBType);

            DBManagementUtils.AddRecordsIgnoreKey(db, sourceRecordsPokemon);
            DBManagementUtils.AddRecordsIgnoreKey(db, sourceRecordsTypes);
            DBManagementUtils.AddRecordsIgnoreKey(db, sourceRecordsJT);
        }
    }

    static void AddSampleData(PokedexContext db)
    {
        Types[] types =
        {
                new() { Name = "Normal", Description = "Pokémon sin afinidad elemental." },
                new() { Name = "Fuego", Description = "Pokémon que dominan el fuego y el calor." },
                new() { Name = "Agua", Description = "Pokémon que controlan el agua y son excelentes nadadores." },
                new() { Name = "Planta", Description = "Pokémon que utilizan la naturaleza y las plantas para combatir." },
                new() { Name = "Eléctrico", Description = "Pokémon que generan electricidad para atacar a sus oponentes." },
                new() { Name = "Hielo", Description = "Pokémon que manipulan el hielo y las bajas temperaturas." },
                new() { Name = "Lucha", Description = "Pokémon expertos en combate cuerpo a cuerpo y fuerza física." },
                new() { Name = "Veneno", Description = "Pokémon que utilizan toxinas y venenos para debilitar a sus enemigos." },
                new() { Name = "Tierra", Description = "Pokémon que manipulan la tierra y los suelos." },
                new() { Name = "Volador", Description = "Pokémon que vuelan y atacan desde el cielo." },
                new() { Name = "Psíquico", Description = "Pokémon que usan habilidades mentales y poderes psíquicos." },
                new() { Name = "Bicho", Description = "Pokémon de tipo insecto, a menudo rápidos y resistentes." },
                new() { Name = "Roca", Description = "Pokémon que tienen cuerpos duros y utilizan rocas para atacar." },
                new() { Name = "Fantasma", Description = "Pokémon espectrales que pueden atravesar objetos y asustar a sus oponentes." },
                new() { Name = "Dragón", Description = "Pokémon poderosos con habilidades de dragón." },
                new() { Name = "Siniestro", Description = "Pokémon que usan tácticas oscuras y engañosas para atacar." },
                new() { Name = "Acero", Description = "Pokémon con cuerpos metálicos que ofrecen gran defensa." },
                new() { Name = "Hada", Description = "Pokémon mágicos con habilidades encantadoras y poderosas." }
            };

        Pokemon[] pokemons =
        {
                new() { Name = "Starly", Weight = 2.0f, Height = 0.3f, Description = "Un Pokémon pequeño y veloz que vuela en grandes bandadas." },
                new() { Name = "Pikachu", Weight = 6.0f, Height = 0.4f, Description = "Famoso por sus potentes ataques eléctricos, Pikachu almacena electricidad en sus mejillas." },
                new() { Name = "Charmander", Weight = 8.5f, Height = 0.6f, Description = "Su cola siempre tiene una llama que refleja su estado emocional." },
                new() { Name = "Bulbasaur", Weight = 6.9f, Height = 0.7f, Description = "Lleva una semilla en su espalda que crece junto a él." },
                new() { Name = "Squirtle", Weight = 9.0f, Height = 0.5f, Description = "Su caparazón lo protege de ataques y le ayuda a nadar a gran velocidad." },
                new() { Name = "Pidgey", Weight = 1.8f, Height = 0.3f, Description = "Un Pokémon muy común que se encuentra en muchas regiones." },
                new() { Name = "Eevee", Weight = 6.5f, Height = 0.3f, Description = "Un Pokémon con un potencial evolutivo increíble." },
                new() { Name = "Machop", Weight = 19.5f, Height = 0.8f, Description = "Este Pokémon entrena constantemente sus músculos para volverse más fuerte." },
                new() { Name = "Gastly", Weight = 0.1f, Height = 1.3f, Description = "Un Pokémon formado principalmente de gas." },
                new() { Name = "Abra", Weight = 19.5f, Height = 0.9f, Description = "Abra pasa la mayor parte de su tiempo durmiendo, pero es capaz de teletransportarse en un instante." },
                new() { Name = "Onix", Weight = 210.0f, Height = 8.8f, Description = "Una enorme serpiente de roca que puede atravesar montañas con facilidad." },
                new() { Name = "Geodude", Weight = 20.0f, Height = 0.4f, Description = "Geodude se camufla como una roca para pasar desapercibido." },
                new() { Name = "Magikarp", Weight = 10.0f, Height = 0.9f, Description = "Un Pokémon débil que no puede hacer mucho más que salpicar." },
                new() { Name = "Jigglypuff", Weight = 5.5f, Height = 0.5f, Description = "Utiliza su canto para dormir a sus oponentes." },
                new() { Name = "Snorlax", Weight = 460.0f, Height = 2.1f, Description = "Snorlax pasa la mayor parte de su tiempo durmiendo." },
                new() { Name = "Dragonite", Weight = 210.0f, Height = 2.2f, Description = "Un Pokémon muy poderoso que es capaz de volar grandes distancias." },
                new() { Name = "Gengar", Weight = 40.5f, Height = 1.5f, Description = "Gengar es muy travieso y disfruta asustando a otros Pokémon y personas." },
                new() { Name = "Lapras", Weight = 220.0f, Height = 2.5f, Description = "Un Pokémon pacífico que suele transportar a personas sobre su espalda." },
                new() { Name = "Psyduck", Weight = 19.6f, Height = 0.8f, Description = "A menudo parece confundido, pero cuando tiene dolores de cabeza, desata poderosas habilidades psíquicas." },
                new() { Name = "Cubone", Weight = 6.5f, Height = 0.4f, Description = "Siempre lleva el cráneo de su madre fallecida." }
            };

        PokemonTypesJT[] pokemonTypes =
        {
                new() { Pokemon = pokemons[0], Type = types[0] },  // Starly - Normal
                new() { Pokemon = pokemons[0], Type = types[9] },  // Starly - Volador
                new() { Pokemon = pokemons[1], Type = types[4] },  // Pikachu - Eléctrico
                new() { Pokemon = pokemons[2], Type = types[1] },  // Charmander - Fuego
                new() { Pokemon = pokemons[3], Type = types[3] },  // Bulbasaur - Planta
                new() { Pokemon = pokemons[3], Type = types[7] },  // Bulbasaur - Veneno
                new() { Pokemon = pokemons[4], Type = types[2] },  // Squirtle - Agua
                new() { Pokemon = pokemons[5], Type = types[0] },  // Pidgey - Normal
                new() { Pokemon = pokemons[5], Type = types[9] },  // Pidgey - Volador
                new() { Pokemon = pokemons[6], Type = types[0] },  // Eevee - Normal
                new() { Pokemon = pokemons[7], Type = types[6] },  // Machop - Lucha
                new() { Pokemon = pokemons[8], Type = types[13] }, // Gastly - Fantasma
                new() { Pokemon = pokemons[8], Type = types[7] },  // Gastly - Veneno
                new() { Pokemon = pokemons[9], Type = types[10] }, // Abra - Psíquico
                new() { Pokemon = pokemons[10], Type = types[12] },// Onix - Roca
                new() { Pokemon = pokemons[10], Type = types[8] }, // Onix - Tierra
                new() { Pokemon = pokemons[11], Type = types[12] },// Geodude - Roca
                new() { Pokemon = pokemons[11], Type = types[8] }, // Geodude - Tierra
                new() { Pokemon = pokemons[12], Type = types[2] }, // Magikarp - Agua
                new() { Pokemon = pokemons[13], Type = types[0] }, // Jigglypuff - Normal
                new() { Pokemon = pokemons[13], Type = types[16] },// Jigglypuff - Hada
                new() { Pokemon = pokemons[14], Type = types[0] }, // Snorlax - Normal
                new() { Pokemon = pokemons[15], Type = types[14] },// Dragonite - Dragón
                new() { Pokemon = pokemons[15], Type = types[9] }, // Dragonite - Volador
                new() { Pokemon = pokemons[16], Type = types[13] },// Gengar - Fantasma
                new() { Pokemon = pokemons[16], Type = types[7] }, // Gengar - Veneno
                new() { Pokemon = pokemons[17], Type = types[2] }, // Lapras - Agua
                new() { Pokemon = pokemons[17], Type = types[5] }, // Lapras - Hielo
                new() { Pokemon = pokemons[18], Type = types[2] }, // Psyduck - Agua
                new() { Pokemon = pokemons[19], Type = types[8] }  // Cubone - Tierra
            };

        db.Pokemons.AddRange(pokemons);
        db.Types.AddRange(types);
        db.PokemonTypesJT.AddRange(pokemonTypes);

        db.SaveChanges();
    }
}