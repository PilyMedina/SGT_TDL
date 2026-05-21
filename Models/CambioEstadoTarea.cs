namespace TDL.Models
{
    public class CambioEstadoTarea
    {
       
            public int ID { get; set; }

            public int ID_tarea { get; set; }
            public Tarea Tarea { get; set; }

            public int ID_estado { get; set; }
            public Estados Estado { get; set; }

            public string Justificacion { get; set; }

            public DateTime FechaCambio { get; set; }
        

    }
}
