using NXWalks.API.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace NXWalks.API.Models.DTO
{
    public class UpdateRequestWalkDTO
    {
        [Required]
        [MaxLength(100,ErrorMessage ="Name Cannot be Exceed From hundered characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Description Cannot be Exceed From Thousand characters")]
        public string Description { get; set; }
        [Required]
        public string LengthInKm { get; set; }
        public string? WalkImgURL { get; set; }
        [Required]
        public Guid DifficultyID { get; set; }
        [Required]
        public Guid RegionID { get; set; }
    }
}
