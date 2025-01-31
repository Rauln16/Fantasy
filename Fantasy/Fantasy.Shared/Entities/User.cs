using Fantasy.Shared.Enums;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Shared.Entities;

public class User : IdentityUser
{
    [Display(Name = "Nombre")]
    [MaxLength(50, ErrorMessageResourceName = "MaxLengthErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Apellido")]
    [MaxLength(50, ErrorMessageResourceName = "MaxLengthErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string LastName { get; set; } = null!;

    [Display(Name = "Tipo de usuario")]
    public UserType UserType { get; set; }

    public Country Country { get; set; } = null!;

    [Display(Name = "Pais")]
    [Range(1, int.MaxValue, ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int CountryId { get; set; }

    [Display(Name = "Usuario")]
    public string FullName => $"{FirstName} {LastName}";
}