namespace GeoEnjoy.Application.Repositories.BlobStorage
{
    public class DeleteBlobObjectDto
    {
        public string BucketName { get; set; } = null!;
        public string ObjectName { get; set; } = null!;
    }
}
