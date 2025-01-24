using System.ComponentModel.DataAnnotations;

namespace Fantasy.Shared.Entities;

public static class ValidationMessages
{
    public static string MaxLengthErrorMessage => "El nombre no puede ser más largo de 100 caracteres";
    public static string RequiredErrorMessage => "Campo requerido";
}

public class Country
{
    public int Id { get; set; }

    [Display(Name = "Pais")]
    [MaxLength(100, ErrorMessageResourceName = "MaxLengthErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string Name { get; set; } = null!;

    public ICollection<Team>? Teams { get; set; }
    public int TeamsCount => Teams == null ? 0 : Teams.Count();
}