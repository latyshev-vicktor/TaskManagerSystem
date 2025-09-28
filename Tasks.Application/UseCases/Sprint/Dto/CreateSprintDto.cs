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
        public long[] FieldActivityIds { get; set; }

        [Required]
        [MinLength(1)]
        public int WeekCount { get; set; }
    }
}
