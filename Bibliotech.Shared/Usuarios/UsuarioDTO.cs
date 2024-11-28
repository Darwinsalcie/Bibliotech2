
using System.ComponentModel.DataAnnotations;


namespace Bibliotech.Shared.Usuarios
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]

        public string EMail { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]

        public int RoleId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string RoleName { get; set; }

        public int? CreationUser { get; set; }
        public DateTime? CreationDate { get; set; }

        public int? ModifyUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? DeleteUser { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
