using Fantasy.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Shared.DTOs;

public class UserDTO : User
{
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener mas de 6 caracteres y menos de 20")]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "La contraseña no coincide")]
    [Display(Name = "Confirmacion contraseña")]
    [DataType(DataType.Password)]
    [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string PasswordConfirm { get; set; } = null!;
}