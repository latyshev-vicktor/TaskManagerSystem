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
        public long FieldActivityId { get; set; }

        [Required]
        public DateTimeOffset StartDate { get; set; }

        [Required]
        public DateTimeOffset EndDate { get; set; }
    }
}
