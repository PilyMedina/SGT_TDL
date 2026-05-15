

using System.ComponentModel.DataAnnotations;

namespace TDL.Models
{
    public class Roles
    {
        [Required]
        public required int ID { get; set; }
        [Required]
        public required string Rol { get; set; }
    }
}
