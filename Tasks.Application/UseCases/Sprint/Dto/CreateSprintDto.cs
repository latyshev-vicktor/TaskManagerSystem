using System.ComponentModel.DataAnnotations;

namespace Tasks.Application.UseCases.Sprint.Dto
{
    public class CreateSprintDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid[] FieldActivityIds { get; set; }

        [Required]
        public int WeekCount { get; set; }
    }
}
