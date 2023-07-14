using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Shared.Entities
{
    public class Seller
    {
        
        public int Id { get; set; }
        [Display(Name ="Vendedor")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string? Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto")]
        public int Mont { get; set; }
        [Display(Name = "Comisión")]
        public int Commission { get; set; }
        public ICollection<Sale>? Sales { get; set; }
    }
}
