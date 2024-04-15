using System.ComponentModel.DataAnnotations;

namespace NXWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has be of Maximum Hundered characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3,ErrorMessage ="Code has be of minimum three characters")]
        [MaxLength(3, ErrorMessage = "Code has be of Maximum three characters")]
        public string Code { get; set; }

        public string? RegionImgURL { get; set; }
    }
}
