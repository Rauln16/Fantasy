using Fantasy.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Shared.DTOs
{
    public class TeamDTO
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
}