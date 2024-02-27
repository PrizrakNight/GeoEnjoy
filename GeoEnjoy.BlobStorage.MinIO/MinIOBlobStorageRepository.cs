using GeoEnjoy.Application.Logging;
using GeoEnjoy.Application.Repositories.BlobStorage;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace GeoEnjoy.BlobStorage.MinIO
{
    public class MinIOBlobStorageRepository(
        IMinioClient client,
        ILogger<MinIOBlobStorageRepository> logger) : IBlobStorageRepository
    {
        public async Task DeleteAsync(DeleteBlobObjectDto deleteBlob,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var args = new RemoveObjectArgs()
                    .WithBucket(deleteBlob.BucketName)
                    .WithObject(deleteBlob.ObjectName);

                await client.RemoveObjectAsync(args, cancellationToken);
            }
            catch (BucketNotFoundException)
            {
                // We don't do anything in this case
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DownloadableBlobObjectDto?> DownloadAsync(DownloadBlobObjectDto downloadBlob,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = new DownloadableBlobObjectDto();

                var args = new GetObjectArgs()
                    .WithBucket(downloadBlob.BucketName)
                    .WithObject(downloadBlob.ObjectName)
                    .WithCallbackStream(s => result.Stream = s);

                var response = await client.GetObjectAsync(args, cancellationToken);

                result.ObjectName = response.ObjectName;
                result.ContentType = response.ContentType;
                result.Size = response.Size;

                return result;
            }
            catch (Exception ex)
            when (ex is BucketNotFoundException || ex is ObjectNotFoundException)
            {
                logger.LogWarning
                (
                    eventId: LoggingEvents.BlobObjectNotFound,
                    exception: ex,
                    message: "The downloadable BlobObject was not found"
                );

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UploadBlobObjectResultDto> UploadAsync(UploadBlobObjectDto uploadBlob,
            CancellationToken cancellationToken = default)
        {
            var args = new PutObjectArgs()
                .WithObject(uploadBlob.ObjectName)
                .WithBucket(uploadBlob.BucketName)
                .WithContentType(uploadBlob.ContentType)
                .WithStreamData(uploadBlob.ObjectStream);

            var response = await client.PutObjectAsync(args, cancellationToken);

            return new UploadBlobObjectResultDto
            {
                ObjectName = uploadBlob.ObjectName,
                ETag = response.Etag,
                Size = response.Size
            };
        }
    }
}
