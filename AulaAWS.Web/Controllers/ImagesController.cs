using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace AulaAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;

        public ImagesController(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage(string nameBucket)
        {
            var response = await _amazonS3.PutBucketAsync(nameBucket);
            return Ok(response);
        }
        [HttpPost("bucket")]
        public async Task<IActionResult> CreateBucket(string nameBucket)
        {
            var response = await _amazonS3.PutBucketAsync(nameBucket);
            return Ok(response);
        }
         [HttpGet("bucket")]
        public async Task<IActionResult> ReadBucket()
        {
            var response = await _amazonS3.ListBucketsAsync();
            return Ok(response.Buckets);
        }
    }
}