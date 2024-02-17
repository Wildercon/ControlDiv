using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ControlDiv.Shared.Enum;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDiv.Shared.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;
        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Mont { get; set; }
        public decimal MontDeliver { get; set; }

    }
}
