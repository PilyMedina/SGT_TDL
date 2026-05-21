

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TDL.Models
{
    [Table("Historial")]
    public class HistorialAccion
    {
        [Key]
        public int ID_historial { get; set; }

        public int ID_tarea { get; set; }

        [ForeignKey("ID_tarea")]
        public Tarea Tarea { get; set; }

        public string Accion { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public int ID_usuario { get; set; }

        [ForeignKey("ID_usuario")]
        public Usuarios Usuario {get; set;}
    }
}
