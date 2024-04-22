using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Utilities
{
    public static class HandleImage
    {
        public static string SetExtensionToBase64(string extension)
        {
            string result = "";

            switch (extension)
            {
                case "jpeg":
                    result = "data:image/jpeg;base64";
                    break;
                case "png":
                    result = "data:image/png;base64";
                    break;
                default:
                    result = "data:image/jpg;base64";
                    break;
            }

            return result;
        }

        public static string GetExtesionFromFilePath(string filePath)
        {
            var name = filePath.Split('.');

            string fileExt = name[1];

            return fileExt;
        }

        public static string GetExtensionFromBase64(string path)
        {
            var contentType = path.Split(",")[0];
            var encode = path.Split(",")[1];
            string extension = "";

            switch (contentType)
            {
                case "data:image/jpeg;base64":
                    extension = "jpeg";
                    break;
                case "data:image/png;base64":
                    extension = "png";
                    break;
                default:
                    extension = "jpg";
                    break;
            }

            return extension;
        }

        public static IFormFile Base64ToImage(string base64String, string fileName)
        {
            var contentTypeTemp = base64String.Split(",")[0];
            var encode = base64String.Split(",")[1];

            var contentTypeTemp_1 = contentTypeTemp.Split(":")[1];
            var contentType = contentTypeTemp_1.Split(";")[0];

            var bytes = Convert.FromBase64String(encode);
            MemoryStream stream = new MemoryStream(bytes);

            string extension = GetExtensionFromBase64(base64String);
            string fullFileName = fileName + "." + extension;

            var file = new FormFile(stream, 0, stream.Length, "0", fullFileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };

            return file;
        }

        public static string ImageToBase64(string path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(path);

            string base64ImageString = SetExtensionToBase64(GetExtesionFromFilePath(path)) + "," + Convert.ToBase64String(imageArray);

            return base64ImageString;
        }
    }
}
