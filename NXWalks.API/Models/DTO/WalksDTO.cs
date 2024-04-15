using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NXWalks.API.Models.DTO
{
    public class WalksDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LengthInKm { get; set; }
        public string? WalkImgURL { get; set; }
   

        //Navigation Property 
        public RegionDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }
    }
}
