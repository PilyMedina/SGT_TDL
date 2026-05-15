
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TDL.Models
{
    public class Tarea
    {
        [Key]
        [Column("ID_tarea")]
        public  int ID_tarea { get; set; }
        [Required]
        public  int ID_estado { get; set; }
        [Required]
        public  int ID_prioridad { get; set; }
        [Required]
        public  string Titulo { get; set; }
        [Required]
        public  string Descripcion { get; set; }
        
        public  string? Justificacion { get; set; }
        [Column("JustificacionCierre")]
        public string? JustificacionCierre { get; set; }
        [Required]
        public  DateTime Fecha_apertura  { get; set; }
        [Required]
        public DateTime Fecha_limite { get; set; }
        [Required]
        public  DateTime Duracion{ get; set; }
        [Column("ID_tecnico")]
        public int ID_tecnico { get; set; }

        [ForeignKey("ID_tecnico")]
        public Usuarios Tecnico { get; set; }
        [ForeignKey("ID_estado")]
        public Estados Estado { get; set; }
        [ForeignKey(nameof(ID_prioridad))]
        public Prioridades Prioridad { get; set; }

    }
}
