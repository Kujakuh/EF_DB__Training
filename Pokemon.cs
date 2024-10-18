using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace EF_DB
{
    public class Pokemon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public required string Name { get; set; }


        public float? Widht { get; set; }
        public float? Height{ get; set; }
        public string? Description { get; set; }

        public required string Type1 { get; set; }
        public string? Type2 { get; set; }

    }
}
