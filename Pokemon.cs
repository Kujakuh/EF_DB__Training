using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace EF_DB
{
    public class Pokemon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Name { get; set; }

        public float? Weight { get; set; }
        public float? Height{ get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Description { get; set; }

        public List<Types>? Types { get; set; }
        public List<PokemonTypesJT>? TypesReference {  get; set; }

    }
}
