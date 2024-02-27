namespace GeoEnjoy.Application.Repositories.BlobStorage
{
    public class UploadBlobObjectDto
    {
        public Stream ObjectStream { get; set; } = null!;

        public string ObjectName { get; set; } = null!;

        public string? BucketName { get; set; }
        public string? ContentType { get; set; }
    }
}
