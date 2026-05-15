

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TDL.Models
{
    public class Estados
    {
        
        [Key]
        public  int ID { get; set; }

        public string Estado{ get; set; }
    }
}
