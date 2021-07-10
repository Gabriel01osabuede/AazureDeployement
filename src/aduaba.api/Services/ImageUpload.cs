using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace aduaba.api.Services
{
    public class ImageUpload
    {
        public static string ImageUploads(string base64String)
        {
            var myAccount = new Account { ApiKey = "394658146624914", ApiSecret = "XhY1ShBFcosd5syu14ZWmTNF4YY", Cloud = "osabuedegabriel" };
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