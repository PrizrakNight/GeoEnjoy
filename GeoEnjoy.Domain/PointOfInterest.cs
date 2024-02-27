namespace GeoEnjoy.Domain
{
    public class PointOfInterest : IDomainEntity
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }

        public bool IsPublic { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        /// <summary>
        /// Point of interest rating from 0.0 to 5.0
        /// </summary>
        public float Rating { get; set; }

        public long Likes { get; set; }
        public long Dislikes { get; set; }
    }
}
