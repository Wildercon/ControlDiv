using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Shared.DTOs
{
    public class ZelleDTO
    {
        [Display(Name = "Codigo")]
        [MaxLength(8, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [MinLength(8, ErrorMessage = "El campo {0} debe tener minimo {1} caractéres.")]
        public string? Codigo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Decimal Mont { get; set; }
        [Display(Name ="Porcentaje")]
        [Range(2, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        public int Percentage { get; set; }
        public string? Details { get; set; }


    }
}
