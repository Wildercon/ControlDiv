using ControlDiv.Shared.Enum;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Shared.Entities
{
    public class Voucher
    {
        public int Id { get; set; }
        [Display(Name = "Codigo")]
        public int Code { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto")]
        public decimal Mont { get; set; } 
        public string? TypeVoucher { get; set; }
        [Display(Name = "Cuenta")]       
        public Account? Account { get; set; }
        public OperationType OperationType { get; set; }

        [Display(Name = "Observación")]
        public string? Observation { get; set; }
        [Display(Name ="Fecha")]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
    }
}
