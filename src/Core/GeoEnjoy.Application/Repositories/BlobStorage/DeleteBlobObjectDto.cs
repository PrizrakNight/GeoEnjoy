namespace GeoEnjoy.Application.Repositories.BlobStorage;

public class DeleteBlobObjectDto
{
    public string ObjectName { get; set; } = null!;
    public string BucketName { get; set; } = null!;
}
