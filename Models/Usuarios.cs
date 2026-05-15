using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TDL.Models
{
    public class Usuarios
    {
        public int ID { get; set; }
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido { get; set; }
        [Required]
        public required string Passsword { get; set; }
        [Required]
        [Column ("Usuario")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public  string Usuario {  get; set; }
        
        [Required]
        [Column ("ID_rol")]
        public  int ID_rol { get; set; }

        [ForeignKey("ID_rol")]
        public Roles Rol { get; set; }

       
    }
}
