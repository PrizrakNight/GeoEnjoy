namespace GeoEnjoy.Application.Repositories.BlobStorage;

public class DownloadableBlobObjectDto
{
    public string ObjectName { get; set; } = null!;
    public string ContentType { get; set; } = null!;

    public long Size { get; set; }

    public Stream? Stream { get; set; }
}
