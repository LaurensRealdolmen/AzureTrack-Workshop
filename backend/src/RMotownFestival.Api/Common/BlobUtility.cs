using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using System;

namespace RMotownFestival.Api.Common
{
    public class BlobUtility
    {
        private readonly StorageSharedKeyCredential _storageSharedKeyCredential;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobSettingsOptions _options;

        public BlobUtility(StorageSharedKeyCredential storageSharedKeyCredential,
            BlobServiceClient blobServiceClient,
            IOptions<BlobSettingsOptions> options)
        {
            _storageSharedKeyCredential = storageSharedKeyCredential;
            _blobServiceClient = blobServiceClient;
            _options = options.Value;
        }

        public BlobContainerClient GetPicturesContainer() => 
            _blobServiceClient.GetBlobContainerClient(_options.PicturesContainer);

        public BlobContainerClient GetThumbsContainer() =>
          _blobServiceClient.GetBlobContainerClient(_options.ThumbsContainer);

        public string GetSasUri(BlobContainerClient container, string blobName)
        {
            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = container.Name,
                BlobName = blobName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-1),
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(2)
            };

            sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);

            var sasToken = sasBuilder.ToSasQueryParameters(_storageSharedKeyCredential).ToString();

            return $"{container.Uri.AbsoluteUri}/{blobName}?{sasToken}";
        }
    }
}
