using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Utilities
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

        public static string ImageToBase64(string path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(path);

            string base64ImageString = SetExtensionToBase64(GetExtesionFromFilePath(path)) + "," + Convert.ToBase64String(imageArray);

            return base64ImageString;
        }
    }
}
