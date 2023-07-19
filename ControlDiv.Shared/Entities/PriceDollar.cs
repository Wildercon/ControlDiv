using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Shared.Entities
{
    public class PriceDollar
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Venta { get; set; }
        [Column(TypeName = "decimal(18,2)")]       
        [DisplayFormat(DataFormatString = "{0:C2}")]        
        public decimal Compra { get; set; }
    }
}
