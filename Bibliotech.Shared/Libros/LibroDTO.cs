
using System.ComponentModel.DataAnnotations;

namespace Bibliotech.Shared.Libros
{
    public class LibroDTO
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Tittle { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]

        public string PictureLink { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Isbn { get; set; }

        public DateTime? PublicationDate { get; set; }

        public bool? Disponibilidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime? ExpireDate { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Status { get; set; }

        public int? CreationUser { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? ModifyUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? DeleteUser { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
