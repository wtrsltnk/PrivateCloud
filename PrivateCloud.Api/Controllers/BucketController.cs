using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrivateCloud.Api.Models;

namespace PrivateCloud.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BucketController :
        ControllerBase
    {
        private readonly ILogger<BucketController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public BucketController(
            ILogger<BucketController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates a new S3 bucket.
        /// To create a bucket, you must register with Amazon S3 and have a valid AWS Access Key ID to authenticate requests. Anonymous requests are never allowed to create buckets. By creating the bucket, you become the bucket owner. 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{bucket}")]
        public CreateBucketResult CreateBucket(
            string bucket,
            CreateBucketRequest request)
        {
            // https://docs.aws.amazon.com/AmazonS3/latest/API/API_CreateBucket.html

            return new CreateBucketResult()
            {
                BucketName = bucket,
            };
        }

        /// <summary>
        /// Deletes the S3 bucket.
        /// All objects (including all object versions and delete markers) in the bucket must be deleted before the bucket itself can be deleted. 
        /// </summary>
        /// <param name="bucket"></param>
        [HttpDelete("{bucket}")]
        public void DeleteBucket(
            string bucket)
        {
            // https://docs.aws.amazon.com/AmazonS3/latest/API/API_DeleteBucket.html

            // this will delete the bucket
        }

        /// <summary>
        /// Removes the null version (if there is one) of an object and inserts a delete marker, which becomes the latest version of the object.
        /// If there isn't a null version, Amazon S3 does not remove any objects. 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        [HttpDelete("{bucket}/{key}")]
        public void DeleteObject(
            string bucket,
            string key)
        {
            // https://docs.aws.amazon.com/AmazonS3/latest/API/API_DeleteObject.html

            // this will delete the object
        }

        /// <summary>
        /// Retrieves objects from Amazon S3.
        /// To use GET, you must have READ access to the object. If you grant READ access to the anonymous user, you can return the object without using an authorization header. 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("{bucket}/{key}")]
        public byte[] GetObject(
            string bucket,
            string key)
        {
            // https://docs.aws.amazon.com/AmazonS3/latest/API/API_GetObject.html

            return new byte[0];
        }

        /// <summary>
        /// This operation is useful to determine if a bucket exists and you have permission to access it.
        /// The operation returns a 200 OK if the bucket exists and you have permission to access it. Otherwise, the operation might return responses such as 404 Not Found and 403 Forbidden. 
        /// </summary>
        /// <param name="bucket"></param>
        [HttpHead("{bucket}")]
        public void HeadBucket(
            string bucket)
        {
            // https://docs.aws.amazon.com/AmazonS3/latest/API/API_HeadBucket.html
        }

        /// <summary>
        /// The HEAD operation retrieves metadata from an object without returning the object itself.
        /// This operation is useful if you're only interested in an object's metadata. To use HEAD, you must have READ access to the object. 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        [HttpHead("{bucket}/{key}")]
        public void HeadObject(
        string bucket,
        string key)
        {
            // https://docs.aws.amazon.com/AmazonS3/latest/API/API_HeadObject.html
        }

        /// <summary>
        /// Returns a list of all buckets owned by the authenticated sender of the request.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ListBucketResult ListBuckets()
        {
            // https://docs.aws.amazon.com/AmazonS3/latest/API/API_ListBuckets.html

            return new ListBucketResult();
        }

        /// <summary>
        /// Returns some or all (up to 1,000) of the objects in a bucket.
        /// You can use the request parameters as selection criteria to return a subset of the objects in a bucket. A 200 OK response can contain valid or invalid XML. Make sure to design your application to parse the contents of the response and handle it appropriately. 
        /// </summary>
        /// <param name="bucket"></param>
        /// <returns></returns>
        [HttpGet("{bucket}")]
        public ListObjectResult ListObjects(
            string bucket)
        {
            // https://docs.aws.amazon.com/AmazonS3/latest/API/API_ListObjectsV2.html

            return new ListObjectResult();
        }

        /// <summary>
        /// Adds an object to a bucket.
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="request"></param>
        [HttpPut("{bucket}/{key}")]
        public void PutObject(
            string bucket,
            string key,
            PutObjectRequest request)
        {
            // https://docs.aws.amazon.com/AmazonS3/latest/API/API_PutObject.html
        }
    }
}
