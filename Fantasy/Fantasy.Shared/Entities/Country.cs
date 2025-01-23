using System.ComponentModel.DataAnnotations;

namespace Fantasy.Shared.Entities;

public class Country
{
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = null!;

    public ICollection<Team>? Teams { get; set; }
    public int TeamsCount => Teams == null ? 0 : Teams.Count();
}