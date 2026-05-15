using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TDL.Models
{
    [Table("TokensRecuperacion")]
    public class TokenRecuperacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_token")]
        public int ID_token { get; set; }

        [Column("Id")]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public bool Usado { get; set; }

        [ForeignKey("Id")]
        public Usuarios Usuario { get; set; }
    }
}