using ControlDiv.Shared.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ControlDiv.Shared.DTOs
{
    public class SaleDTO
    {
        [Display(Name = "Codigo")]
        [MaxLength(12, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [MinLength(11, ErrorMessage = "El campo {0} debe tener minimo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? VoucherCode { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int AccountId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto Operación")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal MontVoucher { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto Venta")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal MontSale { get; set; }
        public string? details { get; set; }
    }
}
