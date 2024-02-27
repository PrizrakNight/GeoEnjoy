namespace GeoEnjoy.Application.Repositories.BlobStorage;

public class DownloadBlobObjectDto
{
    public string ObjectName { get; set; } = null!;
    public string BucketName { get; set; } = null!;
}
