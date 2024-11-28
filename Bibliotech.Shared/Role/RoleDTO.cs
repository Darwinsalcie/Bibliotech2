using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotech.Shared.Role
{
    public class RoleDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Role1 { get; set; }

        public int? CreationUser { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? ModifyUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? DeleteUser { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
