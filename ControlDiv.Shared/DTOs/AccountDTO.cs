using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Shared.DTOs
{
    public class AccountDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Mont {get; set; }
        
        public string? AccountType { get; set; }

    }
}
