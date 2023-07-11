using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture URL")]
        [Required(ErrorMessage = "Profile picture is required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be more than 3 word and less than 50")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Bio is required")]
        public string Bio { get; set; }
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
