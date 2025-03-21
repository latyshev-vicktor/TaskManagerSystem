using System.ComponentModel.DataAnnotations;

namespace Tasks.Application.UseCases.FIeldActivity.Dto
{
    public class CreateFieldActivityDto
    {
        [Required]
        public string Name { get; set; }
    }
}
