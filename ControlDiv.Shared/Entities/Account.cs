using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ControlDiv.Shared.Entities
{
    public class Account
    {
        public int Id { get; set; }
        [Display(Name = "Cuenta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Name { get; set; }
        [Display(Name = "Monto")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Mont { get; set; }
        public string? AccountType { get; set; }
        public ICollection<Voucher>? Vouchers { get; set;}
        
    }
}
