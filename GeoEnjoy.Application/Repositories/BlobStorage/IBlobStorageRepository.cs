namespace GeoEnjoy.Application.Repositories.BlobStorage;

public interface IBlobStorageRepository
{
    Task<UploadBlobObjectResultDto> UploadAsync(UploadBlobObjectDto uploadBlob,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(DeleteBlobObjectDto deleteBlob,
        CancellationToken cancellationToken = default);

    Task<DownloadableBlobObjectDto?> DownloadAsync(DownloadBlobObjectDto downloadBlob,
        CancellationToken cancellationToken = default);
}
