using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMotownFestival.Api.Common;
using System;
using System.Linq;

namespace RMotownFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly BlobUtility _blobUtility;

        public PicturesController(BlobUtility blobUtility)
        {
            _blobUtility = blobUtility;
        }

        [HttpGet]
        public string[] GetAllPictureUrls()
        {
            var container = _blobUtility.GetPicturesContainer();

            return container
                .GetBlobs()
                .Select(blob => _blobUtility.GetSasUri(container, blob.Name))
                .ToArray();
        }

        [HttpPost]
        public void PostPicture(IFormFile file)
        {
            var container = _blobUtility.GetPicturesContainer();
            container.UploadBlob(file.FileName, file.OpenReadStream());
        }
    }
}
