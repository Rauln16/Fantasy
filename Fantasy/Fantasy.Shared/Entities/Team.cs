using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Shared.Entities;

public static class ValidationMessages
{
    public static string MaxLengthErrorMessage => "El nombre no puede ser tan largo";
    public static string RequiredErrorMessage => "Campo requerido";
}

public class Team
{
    public int Id { get; set; }

    [Display(Name = "Equipo")]
    [MaxLength(100, ErrorMessageResourceName = "MaxLengthErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string Name { get; set; } = null!;

    public string? Image { get; set; }
    public Country? Country { get; set; }
    public int CountryId { get; set; }
}