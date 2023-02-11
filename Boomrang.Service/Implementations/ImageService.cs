using System;
using System.Drawing;
using System.IO;

namespace Boomrang.Service.Implementations
{
    public class ImageService
    {
        public string CreateImageUrl(string photoUrl, string photoFileName, string photoLocation)
        {
            try
            {
                var profilePicFile = string.Empty;

                string[] validTypes = { ".pdf", ".docx", ".jpg", ".png", ".jpeg" };

                if (!string.IsNullOrEmpty(photoFileName))
                {
                    int count = 0;

                    foreach (char c in photoFileName)
                    {
                        if ("." == c.ToString())
                            count++;
                    }

                    if (count > 1)
                    {
                        throw new Exception("Invalid file");
                    }
                    profilePicFile = photoFileName;
                }

                if (!string.IsNullOrEmpty(photoUrl))
                {
                    byte[] cardbytes = Convert.FromBase64String(photoUrl);
                    //Image image;
                    string type = Path.GetExtension(photoFileName).ToLower();

                    int pos = Array.IndexOf(validTypes, type);
                    if (pos > -1)
                    {
                        var name = Guid.NewGuid().ToString();
                        profilePicFile = name + type;
                        //using (MemoryStream ms = new MemoryStream(cardbytes))
                        //{
                        //    image = Image.FromStream(ms);
                        //}


                        var cardBase = Convert.FromBase64String(photoUrl);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", photoLocation, profilePicFile);
                        using (var imageFile = new FileStream(path, FileMode.Create))
                        {
                            imageFile.Write(cardbytes, 0, cardBase.Length);
                            imageFile.Flush();
                        }

                    }
                    else
                    {
                        throw new Exception("Invalid file");
                    }


                }
                return profilePicFile;
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
            
        }
    }
}
