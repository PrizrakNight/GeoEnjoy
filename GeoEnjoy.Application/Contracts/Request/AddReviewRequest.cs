namespace GeoEnjoy.Application.Contracts.Request
{
    public class AddReviewRequest
    {
        public string? Comment { get; set; }

        /// <summary>
        /// A rating from the reviewer (From 1 to 5)
        /// </summary>
        public short Rating { get; set; }
    }
}
