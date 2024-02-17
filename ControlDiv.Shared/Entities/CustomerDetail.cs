using ControlDiv.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Shared.Entities
{
    public class CustomerDetail
    {
        public int Id { get; set; }
        public Customer? Customer { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Mont { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public NoteType NoteType { get; set; }

    }
}
