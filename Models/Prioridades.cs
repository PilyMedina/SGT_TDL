

using System.ComponentModel.DataAnnotations;

namespace TDL.Models
{
    public class Prioridades
    {
        [Required]
        public required int ID { get; set; }
        [Required]
        public required string Prioridad { get; set; }
    }
}
