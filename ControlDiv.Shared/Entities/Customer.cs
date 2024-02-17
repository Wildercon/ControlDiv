using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Shared.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Display(Name = "Monto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Name { get; set; }
        [Display(Name = "Monto")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Mont   { get; set; }
        public User? User { get; set; }
    }
}
