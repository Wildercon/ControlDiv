﻿using ControlDiv.Shared.Enum;
using Microsoft.EntityFrameworkCore;
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
        [MaxLength(12, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [MinLength(11, ErrorMessage = "El campo {0} debe tener minimo {1} caractéres.")]
        public string? Code { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Mont { get; set; } 
        
        public string? TypeVoucher { get; set; }
        [Display(Name = "Cuenta")]       
        public Account? Account { get; set; }
        public OperationType OperationType { get; set; }     
        public string? Details { get; set; }
        [Display(Name ="Fecha")]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Total { get; set; }
    }
}
