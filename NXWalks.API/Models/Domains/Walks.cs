namespace NXWalks.API.Models.Domains
{
    public class Walks
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LengthInKm { get; set; }
        public string? WalkImgURL { get; set; }
        public Guid DifficultyID { get; set; }
        public Guid RegionID { get; set; }

        //Navigation Property 
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
