using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AdminBackendApi;

internal partial class Utilities
{
    /// <summary>
    /// Replace tất cả các thẻ html trong chuỗi
    /// </summary>
    internal static string RemoveHTMLTag(string source) => !string.IsNullOrEmpty(source) ? MyRegex().Replace(source, "") : string.Empty;
    [GeneratedRegex("<.*?>")]
    private static partial Regex MyRegex();

    /// <summary>
    /// Tạo mật khẩu theo key salt, mã hóa SHA256
    /// </summary>
    internal static string GeneratePasswordHash(string password, string saltkey)
    {
        byte[] salt = Convert.FromBase64String(saltkey);
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: password,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA1,
        iterationCount: 10000,
        numBytesRequested: 256 / 8));
        return hashed;
    }

    internal static List<string> FolderToListString(string array)
        {
            List<string> lst = [];
            try
            {
                if (!string.IsNullOrEmpty(array))
                {
                    array = array.Trim('/');
                    lst = [.. array.Split('/')];
                }
            }
            catch { }
            return lst.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

    /// <summary>
    /// Tạo Key salt ngẫu nhiên
    /// </summary>
    internal static string GeneratePasswordSalt()
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return Convert.ToBase64String(salt);
    }


    /// <summary>
    /// add logs lỗi vào file 
    /// </summary>
    internal static void AddLogError(Exception ex)
    {
        try
        {
            if (WebConfig.IsLogAdmin)
            {
                string content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-" + ex.Message + ex.StackTrace;
                CreateAppendFile("LogClient_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", content, "Logs");
            }
        }
        catch
        {

        }
    }


    /// <summary>
    /// Tạo file
    /// </summary>
    internal static void CreateAppendFile(string fileName, string content, string path)
    {
        try
        {
            string name = $"{WebConfig.PathServer}{path}/{fileName}";
            string folder = $"{WebConfig.PathServer}{path}";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            FileInfo info = new(name);
            if (info.Exists)
            {
                using StreamWriter writer = info.AppendText();
                try
                {
                    writer.WriteLine(content);
                    writer.Flush();
                    writer.Close();
                }
                catch
                {
                    writer.Flush();
                    writer.Close();
                }
            }
            else
            {
                using StreamWriter writer = info.CreateText();
                try
                {
                    writer.WriteLine(content);
                    writer.Flush();
                    writer.Close();
                }
                catch
                {
                    writer.Flush();
                    writer.Close();
                }
            }

        }
        catch (Exception e)
        {
            AddLogError(e);

        }
    }

    internal static string ReadFile(string fileName, string path)
    {
        string fileContent = string.Empty;
        try
        {
            fileContent = File.ReadAllText(WebConfig.PathServer + path + "/" + fileName);
        }
        catch (Exception e)
        {
            AddLogError(e);
        }
        return fileContent;
    }

    /// <summary>
    /// Xoá file
    /// </summary>
    internal static int DeleteFile(string fileName, string path)
    {
        string name = WebConfig.PathServer + path + "/" + fileName;
        FileInfo info = new(name);
        if (info.Exists)
        {
            info.Delete();
            return 1;
        }
        return 0;
    }

    internal static string GenerateUniqueId()
    {
        HashSet<string> uniqueIds = [];
        Random random = new();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string id;
        do
        {
            string randomPart = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            string dateTimePart = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // Format: yyyyMMddHHmmssfff (e.g., 20230524153030123)
            id = $"{dateTimePart}-{randomPart}";
        } while (!uniqueIds.Add(id));

        return id;
    }


    /// <summary>
    /// replace chuỗi unicode
    /// remove special character 
    /// </summary>
    /// <param name="unicode"></param>
    /// <returns></returns>
    internal static string ConvertRewrite(string unicode)
    {
        try
        {
            if (!string.IsNullOrEmpty(unicode))
            {
                unicode = NewUnicodeToAscii(unicode);
                unicode = unicode.ToLower().Trim();
                unicode = Regex.Replace(unicode, @"\s+", " ");
                unicode = Regex.Replace(unicode, "[\\s]", "-");
                unicode = Regex.Replace(unicode, @"-+", "-");
                unicode = unicode.Replace("®", "");
                unicode = unicode.Replace("™", "");
            }
        }
        catch (Exception e)
        {
            AddLogError(e);
        }
        return unicode;
    }

    /// <summary>
    /// create by BienLV 02-04-2014
    /// remove special character 
    /// </summary>
    /// <param name="unicode"></param>
    /// <returns></returns>
    internal static string NewUnicodeToAscii(string unicode)
    {
        try
        {
            unicode = MyRegex1().Replace(unicode, "a");
            unicode = MyRegex2().Replace(unicode, "e");
            unicode = MyRegex3().Replace(unicode, "u");
            unicode = MyRegex4().Replace(unicode, "i");
            unicode = MyRegex5().Replace(unicode, "o");
            unicode = MyRegex6().Replace(unicode, "d");
            unicode = MyRegex7().Replace(unicode, "y");
            unicode = MyRegex8().Replace(unicode, "");
        }
        catch (Exception e)
        {
            AddLogError(e);
        }
        return unicode;
    }

    internal static int RandomNumber(int min, int max)
    {
        Random random = new();
        return random.Next(min, max);
    }

    [GeneratedRegex("[á|à|ả|ã|ạ|â|ă|ấ|ầ|ẩ|ẫ|ậ|ắ|ằ|ẳ|ẵ|ặ]", RegexOptions.IgnoreCase, "en-VN")]
    private static partial Regex MyRegex1();
    [GeneratedRegex("[é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ]", RegexOptions.IgnoreCase, "en-VN")]
    private static partial Regex MyRegex2();
    [GeneratedRegex("[ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự]", RegexOptions.IgnoreCase, "en-VN")]
    private static partial Regex MyRegex3();
    [GeneratedRegex("[í|ì|ỉ|ĩ|ị]", RegexOptions.IgnoreCase, "en-VN")]
    private static partial Regex MyRegex4();
    [GeneratedRegex("[ó|ò|ỏ|õ|ọ|ô|ơ|ố|ồ|ổ|ỗ|ộ|ớ|ờ|ở|ỡ|ợ]", RegexOptions.IgnoreCase, "en-VN")]
    private static partial Regex MyRegex5();
    [GeneratedRegex("[đ|Đ]", RegexOptions.IgnoreCase, "en-VN")]
    private static partial Regex MyRegex6();
    [GeneratedRegex("[ý|ỳ|ỷ|ỹ|ỵ|Ý|Ỳ|Ỷ|Ỹ|Ỵ]", RegexOptions.IgnoreCase, "en-VN")]
    private static partial Regex MyRegex7();
    [GeneratedRegex("[,|~|@|/|.|:|?|#|$|%|&|*|(|)|+|”|“|'|\"|!|`|–]", RegexOptions.IgnoreCase, "en-VN")]
    private static partial Regex MyRegex8();

}
