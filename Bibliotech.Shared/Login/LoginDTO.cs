using System.ComponentModel.DataAnnotations;

namespace Bibliotech.Shared.Login;
public class LoginModel
{
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string Password { get; set; }
}