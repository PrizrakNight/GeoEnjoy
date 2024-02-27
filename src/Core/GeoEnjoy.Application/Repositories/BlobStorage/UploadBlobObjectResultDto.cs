namespace GeoEnjoy.Application.Repositories.BlobStorage;

public class UploadBlobObjectResultDto
{
    public string ObjectName { get; set; } = null!;
    public string BucketName { get; set; } = null!;

    public string? ContentType { get; set; }

    public string? ETag { get; set; }

    public long Size { get; set; }
}
