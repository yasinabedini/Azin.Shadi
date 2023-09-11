using AutoMapper;
using Azin.Shadi.Core.Convertors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Azin.Shadi.Core.Tools
{
    public static class FileTools
    {
        public static void SaveImage(IFormFile profileImage,string imageName,string whichFolder,bool thumbSave)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{whichFolder}", imageName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                profileImage.CopyTo(stream);
            }

            //save ThumbNail
            string thumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{whichFolder}\\Thumb", imageName);
            ImageConvertor imageConvertor = new ImageConvertor();
            imageConvertor.Image_resize(imagePath, thumbImagePath, 200);
                                                    
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
