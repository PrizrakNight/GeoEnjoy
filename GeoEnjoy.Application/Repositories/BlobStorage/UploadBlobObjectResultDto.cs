namespace GeoEnjoy.Application.Repositories.BlobStorage;

public class UploadBlobObjectResultDto
{
    public string ObjectName { get; set; } = null!;
    public string ETag { get; set; } = null!;

    public long Size { get; set; }
}
