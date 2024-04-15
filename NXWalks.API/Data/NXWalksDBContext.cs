using Microsoft.EntityFrameworkCore;
using NXWalks.API.Models.Domains;
using System.Diagnostics.CodeAnalysis;

namespace NXWalks.API.Data
{
    public class NXWalksDBContext : DbContext
    {
        public NXWalksDBContext(DbContextOptions<NXWalksDBContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walks> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        //On Model Creating method use to seed some data in the database 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding data for difficulties model 

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    ID=Guid.Parse("c1772a09-689d-401b-a906-d97f6752a4c8"),
                    Name="Easy"
                },
                  new Difficulty()
                {
                    ID=Guid.Parse("ce66f4de-b938-4e6d-9822-2ed44d41cbe2"),
                    Name="Medium"
                },
                    new Difficulty()
                {
                    ID=Guid.Parse("8284c498-41f6-4886-9391-e5a01d3ca923"),
                    Name="Hard"
                },
            };
            //this line of code will seed the difficulties to the database 
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Now seeding list of data for Regions Model 
            var Regions = new List<Region>()
            {
                new Region()
                {
                    ID = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImgURL = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                 new Region
                {
                    ID = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImgURL = null
                },
                new Region
                {
                    ID = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImgURL = null
                },
                new Region
                {
                    ID = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImgURL = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    ID = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImgURL = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    ID = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImgURL = null
                },

            };

            modelBuilder.Entity<Region>().HasData(Regions);

            

        }
    }
}
