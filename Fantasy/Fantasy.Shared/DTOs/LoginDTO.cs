using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Shared.DTOs;

public class LoginDTO
{
    [Display(Name = "Email")]
    [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    [EmailAddress(ErrorMessage = "Email no valido")]
    public string Email { get; set; } = null!;

    [Display(Name = "Contraseña")]
    [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MinLength(6, ErrorMessage = "Contraeña demasiado corta")]
    public string Password { get; set; } = null!;
}