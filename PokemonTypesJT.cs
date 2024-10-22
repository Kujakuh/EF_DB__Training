
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EF_DB
{
    public class PokemonTypesJT
    {
        public int PokemonId { get; set; }
        public Pokemon? Pokemon { get; set; }

        public int TypeId { get; set; }
        public Types? Type { get; set; }
    }

}
