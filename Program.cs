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

        List<Pokemon> pokemonList = [];

        using (PokedexContext db = new(dbType.POSTGRE, config))
        {
            //AddSampleData(db);

            pokemonList = [.. db.Pokemons];

            //foreach (var item in pokemonList)
            //{
            //    db.Pokemons.Remove(item);
            //}
        }
        
        using (PokedexContext db = new(dbType.SQLSERVER, config))
        {
            foreach (var item in pokemonList)
            {
                //container = new Pokemon(
                //    item.Name, 
                //    item.Type1,
                //    item.Type2,
                //    item.Height,
                //    item.Widht,
                //    item.Description
                //    );

                //db.Pokemons.Add(item);

            }
            db.SaveChanges();
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

        for (int i = 0; i < pokemons.Length; i++) db.Pokemons.Add(pokemons[i]);

        db.SaveChanges();

    }

}