using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace AdminBackendApi;

internal class HandleFiles
{

    internal static UploadFileModel UploadFiles(IFormFile[] files, string type)
    {
        UploadFileModel msg = new()
        {
            IsError = true,
            Message = "Upload file thất bại :)"
        };
        try
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string newDirectory = Path.Combine(WebConfig.PathServer!, currentDate);
            if (!Directory.Exists(newDirectory))
            {
                Directory.CreateDirectory(newDirectory);
            }
            foreach (var file in files)
            {
                if (file.Length <= 0)
                {
                    msg.Message = "Chưa nhập file :)";
                    return msg;
                };
                if (type == "image")
                {
                    if (!ImageTypes.Contains(file.ContentType))
                    {
                        msg.Message = "File không đúng định dạng (chỉ nhận file jpg,png,webp,svg,gif) :)";
                        return msg;
                    }
                    if (file.Length > 1 * 1024 * 1024) //mb
                    {
                        msg.Message = "File không được lớn hơn 1MB";
                        return msg;
                    }
                }
                if (type == "file")
                {
                    if (!FileTypes.Contains(file.ContentType))
                    {
                        msg.Message = "File không đúng định dạng (chỉ nhận file pdf,doc,docx,xl)";
                        return msg;
                    }
                    if (file.Length > 10 * 1024 * 1024) //mb
                    {
                        msg.Message = "File không được lớn hơn 10MB";
                        return msg;
                    }
                }
                if (type == "VideoTypes")
                {
                    if (!FileTypes.Contains(file.ContentType))
                    {
                        msg.Message = "File không đúng định dạng (chỉ nhận file mp4,flv)";
                        return msg;
                    }

                    if (file.Length > 50 * 1024 * 1024) //mb
                    {
                        msg.Message = "File không được lớn hơn 50MB";
                        return msg;
                    }
                }
                FileInfo f = new(file.FileName);
                string filename = MakeUniqueFilename(newDirectory, f.Name);
                string dest = Path.Combine(newDirectory, filename);
                if (ImageTypes.Contains(file.ContentType))
                {
                    using var stream = file.OpenReadStream();
                    using var output = new MemoryStream();
                    using Image image = Image.Load(stream);
                    //add watermark to image
                    // using (var watermark = SixLabors.ImageSharp.Image.Load($"{_systemRootPath}/wwwroot/watermark/watermark.png")) // Thay đổi đường dẫn đến tệp logo watermark của bạn
                    // {
                    // image.Mutate(x => x
                    // .DrawImage(watermark, new SixLabors.ImageSharp.Point(15, 15), 1f)); // Điều chỉnh vị trí và độ trong suốt của watermark
                    // }
                    image.Save(dest);
                }
                else
                {
                    using FileStream saveFile = new(dest, FileMode.Create);
                    file.CopyTo(saveFile);
                    saveFile.Close();
                    saveFile.Dispose();
                }
            }
            msg.IsError = false;
            msg.Message = "Upload file thành công :3";
            return msg;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return msg;
        }
    }

    internal static UploadFileModel UploadFile(IFormFile file, string type)
    {
        UploadFileModel msg = new()
        {
            IsError = true,
            Message = "Upload file thất bại :)"
        };
        try
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string newDirectory = Path.Combine(WebConfig.PathServer! + "uploads/", currentDate);
            string path = "/uploads/" + currentDate + "/";
            if (!Directory.Exists(newDirectory))
            {
                Directory.CreateDirectory(newDirectory);
            }
            if (file.Length <= 0)
            {
                msg.Message = "Chưa nhập file :)";
                return msg;
            };
            if (type == "image")
            {
                if (!ImageTypes.Contains(file.ContentType))
                {
                    msg.Message = "File không đúng định dạng (chỉ nhận file jpg,png,webp,svg,gif) :)";
                    return msg;
                }
                if (file.Length > 1 * 1024 * 1024) //mb
                {
                    msg.Message = "File không được lớn hơn 1MB";
                    return msg;
                }
            }
            if (type == "file")
            {
                if (!FileTypes.Contains(file.ContentType))
                {
                    msg.Message = "File không đúng định dạng (chỉ nhận file pdf,doc,docx,xl)";
                    return msg;
                }
                if (file.Length > 10 * 1024 * 1024) //mb
                {
                    msg.Message = "File không được lớn hơn 10MB";
                    return msg;
                }
            }
            if (type == "VideoTypes")
            {
                if (!FileTypes.Contains(file.ContentType))
                {
                    msg.Message = "File không đúng định dạng (chỉ nhận file mp4,flv)";
                    return msg;
                }

                if (file.Length > 50 * 1024 * 1024) //mb
                {
                    msg.Message = "File không được lớn hơn 50MB";
                    return msg;
                }
            }
            FileInfo f = new(file.FileName);
            string filename = MakeUniqueFilename(newDirectory, f.Name);
            string dest = Path.Combine(newDirectory, filename);
            if (ImageTypes.Contains(file.ContentType))
            {
                using var stream = file.OpenReadStream();
                using var output = new MemoryStream();
                using Image image = Image.Load(stream);
                //add watermark to image
                // using (var watermark = SixLabors.ImageSharp.Image.Load($"{_systemRootPath}/wwwroot/watermark/watermark.png")) // Thay đổi đường dẫn đến tệp logo watermark của bạn
                // {
                // image.Mutate(x => x
                // .DrawImage(watermark, new SixLabors.ImageSharp.Point(15, 15), 1f)); // Điều chỉnh vị trí và độ trong suốt của watermark
                // }
                image.Save(dest);
            }
            else
            {
                using FileStream saveFile = new(dest, FileMode.Create);
                file.CopyTo(saveFile);
                saveFile.Close();
                saveFile.Dispose();
            }
            msg.IsError = false;
            msg.Message = "Upload file thành công :3";
            msg.UrlPicture = path + filename;
            return msg;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return msg;
        }
    }

    internal static bool ResizeImage(string pathSave, string pathImage, string size, string name)
    {
        try
        {
            Byte[] bytes = File.ReadAllBytes(pathImage);
            string[] listsize = size.Split(',');
            var ext = Path.GetExtension(name).ToLower();
            if (ext == ".jpeg" || ext == ".jpg" || ext == ".png")
            {
                for (int i = 0; i < listsize.Length; i++)
                {
                    string newurl = AddTail(pathSave + "/", Path.GetFileNameWithoutExtension(name), "x" + listsize[i] + "x4", ext, listsize[i]);
                    using Image image = Image.Load(pathImage);
                    int height;
                    int width = Convert.ToInt32(listsize[i]);
                    if (image.Width > width)
                    {
                        height = (int)Math.Round(Convert.ToDecimal(width * image.Height / image.Width));
                    }
                    else
                    {
                        width = image.Width;
                        height = image.Height;
                    }
                    image.Mutate(x => x.Resize(width, height));
                    image.Save(newurl);
                }
            }
            return true;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return false;
        }

    }

    private static string AddTail(string pathurl, string name, string tail, string extention, string s)
    {
        string str = string.Empty;
        try
        {
            string url = pathurl.Replace("/uploads/", "/resizes/" + s + "/");
            string path = Path.GetDirectoryName(url)!;
            bool exists = Directory.Exists(url);
            if (!exists)
                Directory.CreateDirectory(url);
            string link = url + name + tail + extention;
            return link;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
        }
        return str;
    }

    private static readonly List<string> FileTypes =
    [
        "application/msword","application/vnd.openxmlformats-officedocument.wordprocessingml.document","application/vnd.ms-excel",
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","application/pdf"
    ];

    private static readonly List<string> ImageTypes =
    [
        "image/png", "image/gif", "image/apng", "image/jpeg", "image/svg+xml", "image/webp"
    ];
    private static readonly List<string> VideoTypes =
    [
      "video/mp4","video/x-flv"
    ];

    private static string MakeUniqueFilename(string dir, string filename)
    {
        string ret = filename;
        int i = 0;
        if (File.Exists(Path.Combine(dir, ret)))
        {
            i++;
            var name = Path.GetFileNameWithoutExtension(filename);
            ret = Utilities.ConvertRewrite(name) + "-copy" + i.ToString() + Path.GetExtension(filename);
        }
        else
        {
            var name = Path.GetFileNameWithoutExtension(filename);
            ret = Utilities.ConvertRewrite(name) + Path.GetExtension(filename);
        }
        return ret;
    }


}
