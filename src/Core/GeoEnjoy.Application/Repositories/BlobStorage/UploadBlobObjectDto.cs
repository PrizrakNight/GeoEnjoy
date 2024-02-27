namespace GeoEnjoy.Application.Repositories.BlobStorage;

public class UploadBlobObjectDto
{
    public string ObjectName { get; set; } = null!;
    public string BucketName { get; set; } = null!;

    public string? ContentType { get; set; }

    public Stream ObjectStream { get; set; } = null!;
}
