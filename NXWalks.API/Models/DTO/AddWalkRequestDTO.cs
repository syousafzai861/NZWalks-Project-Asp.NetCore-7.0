using System.ComponentModel.DataAnnotations;

namespace NXWalks.API.Models.DTO
{
    public class AddWalkRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name Should be Of 100 Characters Only")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Description Should be Of 1000 Characters Only")]
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
