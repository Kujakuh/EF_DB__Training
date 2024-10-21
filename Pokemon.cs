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


        public float? Widht { get; set; }
        public float? Height{ get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Description { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Type1 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? Type2 { get; set; }

        [SetsRequiredMembers]
        public Pokemon(string name, string type1) 
        {
            Name = name;
            Type1 = type1;
        }

        [SetsRequiredMembers]
        public Pokemon(string name, string type1, string? type2, float? height, float? widht, string? description)
        {
            Name = name;
            Type1 = type1;
            Type2 = type2;
            Height = height;
            Widht = widht;
            Description = description;

        }
    }
}
