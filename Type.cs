using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace EF_DB
{
    public class Types
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Name { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Description { get; set; }

    }
}
