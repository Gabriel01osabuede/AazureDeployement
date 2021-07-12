using System;
using aduaba.api.Interface;
using aduaba.api.Utility;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace aduaba.api.Services
{
    public class ImageUpload : IImageUpload
    {
        private readonly IOptions<CloudinarySettings> _settings;

        public ImageUpload(
            IOptions<CloudinarySettings> settings
        )
        {
            _settings = settings;
        }

        public string ImageUploads(string base64String)
        {
            var apikey = _settings.Value.CloudinaryApiKey;
            var secret = _settings.Value.CloudinarySecret;
            var cloudName = _settings.Value.CloudinaryCloudName;

            var myAccount = new Account { ApiKey = apikey, ApiSecret = secret, Cloud = cloudName };
            Cloudinary _cloudinary = new Cloudinary(myAccount);
            _cloudinary.Api.Secure = true;

            var constant = @"data:image/png;base64,";
            var fullImagePath = constant + base64String;

            var uploadParams = new ImageUploadParams()
            {

                File = new FileDescription(@fullImagePath),
                Folder = "AduabaFresh/imageFolder"
            };
            var uploadResult = _cloudinary.Upload(@uploadParams);

            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public static string GetBase64StringForImage(string imgPath)  
        {  
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);  
            string base64String = Convert.ToBase64String(imageBytes);  
            return base64String;  
        } 
    }
}