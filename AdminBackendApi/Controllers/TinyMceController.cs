// using System.Collections;
// using System.IO.Compression;
// using System.Text;
// using AdminBackendApi.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace AdminBackendApi.Controllers
// {
//     public partial class TinyMceController : BaseController
//     {
//         private readonly string _systemRootPath;
//         private readonly string _tempPath;
//         private readonly string _filesRootPath;
//         private readonly string _filesRootVirtual;

//         public TinyMceController(IWebHostEnvironment env)
//         {
//             // Setup CMS paths to suit your environment (we usually inject settings for these)
//             _systemRootPath = env.ContentRootPath;
//             _tempPath = _systemRootPath + "/wwwroot/Uploads/Temp";
//             _filesRootPath = "/wwwroot/Uploads";
//             _filesRootVirtual = "/Uploads";
//         }

//         readonly MessagesModel msg = new()
//         {
//             Message = "Tải thất bại :)"
//         };

//         #region API Actions
//         #region  Dir

//         [HttpPost("DirList")]
//         [Authorize]
//         public IActionResult DIRLIST(string? type)
//         {
//             try
//             {
//                 DirectoryInfo d = new(GetFilesRoot());
//                 if (!d.Exists)
//                 {
//                     msg.Message = "Không tồn tại folder root uploads :)";
//                     throw new Exception(msg.Message);
//                 }
//                 ArrayList dirs = ListDirs(d.FullName);
//                 dirs.Insert(0, d.FullName);
//                 string localPath = _systemRootPath;
//                 string result = string.Empty;
//                 for (int i = 0; i < dirs.Count; i++)
//                 {
//                     string dir = (string)dirs[i]!;
//                     result += (result != string.Empty ? "," : string.Empty) + "{\"p\":\"" + MakeVirtualPath(dir.Replace(localPath, string.Empty).Replace("\\", "/")) + "\",\"f\":\"" + GetFiles(dir, type!).Count.ToString() + "\",\"d\":\"" + Directory.GetDirectories(dir).Length.ToString() + "\"}";
//                 }
//                 return Content("[" + result + "]", "application/json");
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         [HttpPost("CopyDir")]
//         [Authorize]
//         public IActionResult COPYDIR(string d, string n)
//         {
//             try
//             {
//                 d = MakePhysicalPath(d);
//                 n = MakePhysicalPath(n);
//                 CheckPath(d);
//                 CheckPath(n);
//                 DirectoryInfo dir = new(FixPath(d));
//                 DirectoryInfo newDir = new(FixPath(n + "/" + dir.Name));
//                 if (!dir.Exists)
//                 {
//                     msg.Message = "Thư mục không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 else if (newDir.Exists)
//                 {
//                     msg.Message = "Thư mục mới dã tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 else CopyDir(dir.FullName, newDir.FullName);
//                 msg.Message = "Copy thư mục thành công";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         [HttpPost("CreateDir")]
//         [Authorize]
//         public IActionResult CREATEDIR(string d, string n)
//         {
//             try
//             {
//                 d = MakePhysicalPath(d);
//                 CheckPath(d);
//                 d = FixPath(d);
//                 n = Utilities.ConvertRewrite(n);
//                 d = Path.Combine(d, n);
//                 if (!Directory.Exists(d)) Directory.CreateDirectory(d);
//                 msg.Message = "Tạo thư mục thành công <3";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         [HttpPost("DeleteDir")]
//         [Authorize]
//         public IActionResult DELETEDIR(string d)
//         {
//             try
//             {
//                 d = MakePhysicalPath(d);
//                 CheckPath(d);
//                 d = FixPath(d);
//                 if (!Directory.Exists(d))
//                 {
//                     msg.Message = "Thư mục không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 if (d == GetFilesRoot())
//                 {
//                     msg.Message = "Không thể xoá thư mục gốc";
//                     throw new Exception(msg.Message);
//                 }
//                 if (Directory.GetDirectories(d).Length > 0 || Directory.GetFiles(d).Length > 0)
//                 {
//                     msg.Message = "Xoá hết file thì mới xoá được thư mục";
//                     throw new Exception(msg.Message);
//                 }
//                 Directory.Delete(d);
//                 msg.Message = "Xoá thư mục thành công";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         [HttpGet("DownloadDir")]
//         public ActionResult DOWNLOADDIR(string d)
//         {
//             try
//             {
//                 d = MakePhysicalPath(d);
//                 d = FixPath(d);
//                 if (!Directory.Exists(d))
//                 {
//                     msg.Message = "Thư mục không tồn tai";
//                     throw new Exception(msg.Message);
//                 }
//                 string dirName = new FileInfo(d).Name;
//                 string tmpZip = _tempPath + "/" + dirName + ".zip";
//                 if (!Directory.Exists(_tempPath)) Directory.CreateDirectory(_tempPath);
//                 if (System.IO.File.Exists(tmpZip)) System.IO.File.Delete(tmpZip);
//                 ZipFile.CreateFromDirectory(d, tmpZip, CompressionLevel.Fastest, true);
//                 return PhysicalFile(tmpZip, "application/zip", dirName + ".zip");
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }
//         [HttpPost("MoveDir")]
//         [Authorize]
//         public IActionResult MOVEDIR(string d, string n)
//         {
//             try
//             {
//                 d = MakePhysicalPath(d);
//                 n = MakePhysicalPath(n);
//                 CheckPath(d);
//                 CheckPath(n);
//                 DirectoryInfo source = new(FixPath(d));
//                 DirectoryInfo dest = new(FixPath(Path.Combine(n, source.Name)));
//                 if (dest.FullName.StartsWith(source.FullName))
//                 {
//                     msg.Message = "Không thể di chuyển thư mục vào thư mục con của chính nó";
//                     throw new Exception(msg.Message);
//                 }
//                 if (!source.Exists)
//                 {
//                     msg.Message = "Thư mục không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 if (dest.Exists)
//                 {
//                     msg.Message = "Thư mục đã tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 source.MoveTo(dest.FullName);
//                 msg.Message = "Di chuyển thư mục thành công";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }
//         [HttpPost("RenameDir")]
//         [Authorize]
//         public IActionResult RENAMEDIR(string d, string n)
//         {
//             try
//             {
//                 d = MakePhysicalPath(d);
//                 CheckPath(d);
//                 DirectoryInfo source = new(FixPath(d));
//                 DirectoryInfo dest = new(Path.Combine(source.Parent!.FullName, n));
//                 if (source.FullName == GetFilesRoot())
//                 {
//                     msg.Message = "Bạn không thể đổi tên thư mục gốc";
//                     throw new Exception(msg.Message);
//                 }
//                 if (!source.Exists)
//                 {
//                     msg.Message = "Thư mục không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 if (dest.Exists)
//                 {
//                     msg.Message = "Tên thư mục đã tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 source.MoveTo(dest.FullName);
//                 msg.Message = "Đổi tên thư mục thành công";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }


//         #endregion

//         #region  file
//         [HttpPost("FileList")]
//         [Authorize]
//         public IActionResult FILESLIST(string? d, string? type)
//         {
//             try
//             {
//                 d = MakePhysicalPath(d!);
//                 CheckPath(d);
//                 string fullPath = FixPath(d);
//                 List<string> files = GetFiles(fullPath, type!);
//                 StringBuilder result = new();
//                 for (int i = 0; i < files.Count; i++)
//                 {
//                     FileInfo f = new(files[i]);
//                     int w = 0, h = 0;
//                     result.Append(result.ToString() != string.Empty ? "," : string.Empty);
//                     result.Append('{');
//                     result.Append("\"p\":\"" + MakeVirtualPath(d) + "/" + f.Name + "\"");
//                     result.Append(",\"t\":\"" + Math.Ceiling(LinuxTimestamp(f.LastWriteTime)).ToString() + "\"");
//                     result.Append(",\"s\":\"" + f.Length.ToString() + "\"");
//                     result.Append(",\"w\":\"" + w.ToString() + "\"");
//                     result.Append(",\"h\":\"" + h.ToString() + "\"");
//                     result.Append('}');
//                 }
//                 return result.Length > 0 ? Content("[" + result.ToString() + "]") : Content("");
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         [HttpPost("CopyFile")]
//         [Authorize]
//         public IActionResult COPYFILE(string f, string n)
//         {
//             try
//             {
//                 f = MakePhysicalPath(f);
//                 n = MakePhysicalPath(n);
//                 CheckPath(f);
//                 CheckPath(n);
//                 FileInfo file = new(FixPath(f));
//                 n = FixPath(n);
//                 if (!file.Exists)
//                 {
//                     msg.Message = "File không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 if (!Directory.Exists(n))
//                 {
//                     msg.Message = "Thư mục sao chép không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 System.IO.File.Copy(file.FullName, Path.Combine(n, MakeUniqueFilename(n, file.Name)));
//                 msg.Message = "Sao chép file thành công";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         [HttpPost("DeleteFile")]
//         [Authorize]
//         public IActionResult DELETEFILE(string f)
//         {
//             //var d = "/wwwroot/resize/";
//             List<string> list = Utilities.FolderToListString(f);
//             int size = list.Count;
//             string name = list[size - 1];
//             try
//             {
//                 f = MakePhysicalPath(f);
//                 CheckPath(f);
//                 f = FixPath(f);
//                 FileInfo file = new(f);
//                 string path = f.Replace("/" + name, "/");
//                 if (!file.Exists)
//                 {
//                     msg.Message = "File không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 System.IO.File.Delete(f);
//                 #region delete resize
//                 string? sizes = WebConfig.Sizes;
//                 string[] listsize = sizes!.Split(',');
//                 foreach (string item in listsize)
//                 {
//                     string resizePath = path.Replace("/Uploads/", "/resize/" + item + "/");
//                     string pathmobile = resizePath + Path.GetFileNameWithoutExtension(name) + "x" + item + "x4" + Path.GetExtension(name);
//                     if (System.IO.File.Exists(pathmobile)) System.IO.File.Delete(pathmobile);
//                 }
//                 #endregion
//                 msg.Message = "Xoá file thành công";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         [HttpGet("Download")]
//         public async Task<ActionResult> DOWNLOAD(string f)
//         {
//             try
//             {
//                 f = MakePhysicalPath(f);
//                 CheckPath(f);
//                 FileInfo file = new(FixPath(f));
//                 if (!file.Exists)
//                 {
//                     msg.Message = "File không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 MemoryStream memory = new();
//                 using (FileStream stream = new(file.FullName, FileMode.Open))
//                 {
//                     await stream.CopyToAsync(memory);
//                 }
//                 memory.Position = 0;
//                 // _ = new FileExtensionContentTypeProvider().TryGetContentType(file.FullName, out var contentType);
//                 // return PhysicalFile(file.Name, contentType ?? "application/octet-stream", file.Name);
//                 return File(memory, GetContentType(file.FullName), Path.GetFileName(file.FullName));
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }
//         [HttpPost("MoveFile")]
//         [Authorize]
//         public IActionResult MOVEFILE(string f, string n)
//         {
//             try
//             {
//                 f = MakePhysicalPath(f);
//                 n = MakePhysicalPath(n);
//                 CheckPath(f);
//                 CheckPath(n);
//                 FileInfo source = new(FixPath(f));
//                 FileInfo dest = new(FixPath(n));
//                 if (!source.Exists)
//                 {
//                     msg.Message = "File không tồn tại";
//                     throw new Exception(msg.Message);
//                 }

//                 if (dest.Exists)
//                 {
//                     msg.Message = "File đã tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 source.MoveTo(dest.FullName);
//                 msg.Message = "Di chuyển File thành công";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         [HttpPost("RenameFile")]
//         [Authorize]
//         public IActionResult RENAMEFILE(string f, string n)
//         {
//             try
//             {
//                 f = MakePhysicalPath(f);
//                 CheckPath(f);
//                 FileInfo source = new(FixPath(f));
//                 FileInfo dest = new(Path.Combine(source.Directory!.FullName, n));
//                 if (!source.Exists)
//                 {
//                     msg.Message = "File không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 source.MoveTo(dest.FullName);
//                 msg.Message = "Đổi tên File thành công";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         [HttpPost("upload")]
//         [Authorize]
//         public IActionResult UPLOAD(string? d, IFormFileCollection files)
//         {
//             try
//             {
//                 if (string.IsNullOrEmpty(d))
//                 {
//                     msg.Message = "Đường dẫn không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 d = MakePhysicalPath(d!);
//                 CheckPath(d!);
//                 d = FixPath(d!);
//                 if (!Directory.Exists(d))
//                 {
//                     msg.Message = "Thư mục upload không tồn tại";
//                     throw new Exception(msg.Message);
//                 }
//                 foreach (IFormFile file in files)
//                 {
//                     if (file.Length <= 0)
//                     {
//                         msg.Message = "Chưa nhập file";
//                         throw new Exception(msg.Message);
//                     };
//                     if (!FileType.Contains(file.ContentType))
//                     {
//                         msg.Message = "File không đúng đinh dạng";
//                         throw new Exception(msg.Message);
//                     }
//                     if (file.Length > 20 * 1024 * 1024)
//                     {
//                         msg.Message = "File không được quá 20mb";
//                         throw new Exception(msg.Message);
//                     }
//                     FileInfo f = new(file.FileName);
//                     string filename = MakeUniqueFilename(d!, f.Name);
//                     string dest = Path.Combine(d!, filename);
//                     #region image default
//                     using FileStream saveFile = new(dest, FileMode.Create);
//                     file.CopyTo(saveFile);
//                     saveFile.Close();
//                     saveFile.Dispose();
//                     AddLogAdmin("upload-file-manager", "upload File: vào:" + d, "Upload-File");
//                     #endregion
//                 }
//                 msg.Message = "Upload file thành công";
//                 return Ok(msg);
//             }
//             catch (Exception ex)
//             {
//                 Utilities.AddLogError(ex);
//                 return NotFound(msg);
//             }
//         }

//         #endregion
//         public static readonly List<string> FileType =
//         [
//             "image/png", "image/gif", "image/avif", "image/apng", "image/jpeg", "image/svg+xml", "image/webp", "image/vnd.microsoft.icon",
//             "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "text/csv", "application/x-7z-compressed", "application/vnd.rar",  "audio/mpeg",
//             "video/mp4", "video/x-msvideo", "audio/wav","video/mpeg"
//         ];
//         #endregion

//         #region Utilities
//         private string MakeVirtualPath(string path)
//         {
//             return !path.StartsWith(_filesRootPath) ? path : _filesRootVirtual + path.Substring(_filesRootPath.Length);
//         }

//         private string MakePhysicalPath(string path)
//         {
//             return !path.StartsWith(_filesRootVirtual) ? path : _filesRootPath + path.Substring(_filesRootVirtual.Length);
//         }

//         private string GetFilesRoot()
//         {
//             string ret = _filesRootPath;
//             ret = FixPath(ret!);
//             return ret;
//         }

//         private static ArrayList ListDirs(string path)
//         {
//             string[] dirs = Directory.GetDirectories(path);
//             ArrayList ret = [];
//             foreach (string dir in dirs)
//             {
//                 ret.Add(dir);
//                 ret.AddRange(ListDirs(dir));
//             }
//             return ret;
//         }

//         private static List<string> GetFiles(string path, string type)
//         {
//             List<string> ret = [];
//             if (type == "#" || type == null) type = string.Empty;
//             string[] files = Directory.GetFiles(path);
//             foreach (string f in files) { if (GetFileType(new FileInfo(f).Extension) == type || type == string.Empty) ret.Add(f); }
//             return ret;
//         }

//         private static string GetFileType(string ext)
//         {
//             string ret = "file";
//             ext = ext.ToLower();
//             if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif") ret = "image";
//             else if (ext == ".swf" || ext == ".flv") ret = "flash";
//             return ret;
//         }

//         private void CheckPath(string path)
//         {
//             if (!FixPath(path).StartsWith(GetFilesRoot())) throw new Exception("Access to " + path + " is denied");
//         }

//         private string FixPath(string path)
//         {
//             path = path.TrimStart('~');
//             if (!path.StartsWith("/")) path = "/" + path;
//             return _systemRootPath + path;
//         }

//         private static double LinuxTimestamp(DateTime d)
//         {
//             DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
//             TimeSpan timeSpan = d.ToLocalTime() - epoch;
//             return timeSpan.TotalSeconds;
//         }


//         private static void CopyDir(string path, string dest)
//         {
//             if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
//             foreach (string f in Directory.GetFiles(path))
//             {
//                 FileInfo file = new(f);
//                 if (!System.IO.File.Exists(Path.Combine(dest, file.Name))) System.IO.File.Copy(f, Path.Combine(dest, file.Name));
//             }
//             foreach (string d in Directory.GetDirectories(path)) CopyDir(d, Path.Combine(dest, new DirectoryInfo(d).Name));
//         }

//         private static string MakeUniqueFilename(string dir, string filename)
//         {
//             string ret = filename;
//             int i = 0;
//             if (System.IO.File.Exists(Path.Combine(dir, ret)))
//             {
//                 i++;
//                 var name = Path.GetFileNameWithoutExtension(filename);
//                 ret = Utilities.ConvertRewrite(name) + "-copy" + i.ToString() + Path.GetExtension(filename);
//             }
//             else
//             {
//                 var name = Path.GetFileNameWithoutExtension(filename);
//                 ret = Utilities.ConvertRewrite(name) + Path.GetExtension(filename);
//             }
//             return ret;
//         }
//         #endregion
//     }
// }