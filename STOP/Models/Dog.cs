using System.ComponentModel.DataAnnotations;

namespace STOP.Models
{
    public class Dog
    {
        public Guid DogId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The dog's name can't be shorter than {2} and longer than {1}", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The dog's description can't be shorter than {2} and longer than {1}", MinimumLength = 2)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        public string? OwnerId { get; set; }

        public virtual ApplicationUser? Owner { get; set; }
    }
}
