using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Shared.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        [Display(Name = "Vendedor")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Seller? Seller { get; set; }
        public string? Details { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Mont { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public DateTime Date { get; set; } 

    }
}
