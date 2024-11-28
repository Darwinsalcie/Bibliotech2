
using System.ComponentModel.DataAnnotations;


namespace Bibliotech.Shared.Reserva
{
    public class ReservaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]

        public int UserId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]

        public int BookId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]

        public DateTime? ReservationDate { get; set; }


        public string? Status { get; set; }

        public int? CreationUser { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? ModifyUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? DeleteUser { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
