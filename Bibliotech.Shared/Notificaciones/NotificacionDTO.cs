using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Bibliotech.Shared.Notificaciones
{
    public class NotificacionDTO
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Message { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime FechaEnvio { get; set; }

        public int? CreationUser { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? ModifyUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? DeleteUser { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
