using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace aduaba.api.Services
{
    public class ImageUpload
    {
        public static string ImageUploads(string imagePath)
        {
            var myAccount = new Account { ApiKey = "394658146624914", ApiSecret = "XhY1ShBFcosd5syu14ZWmTNF4YY", Cloud = "osabuedegabriel" };
            Cloudinary _cloudinary = new Cloudinary(myAccount);

            var uploadParams = new ImageUploadParams()
            {

                File = new FileDescription(imagePath)
            };
            var uploadResult = _cloudinary.Upload(uploadParams);


            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}