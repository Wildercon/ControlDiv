using ControlDiv.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Shared.DTOs
{
    public class DeliverOrReceiveDTO
    {
        [Display(Name ="Operación")]
        
        public bool IsDeliver { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Mont { get; set; }
        public string? Details { get; set; }
        [Display(Name ="Vendedor")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? IdUser { get; set; }
    }
}
