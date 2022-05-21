using System.Net;

namespace webserver
{
    public class Path
    {
        public static string GetRequestedPath(HttpListenerRequest request, string rootDir)
        {
            string path = String.Concat(rootDir.Replace(" ", "\\ "), request?.Url?.LocalPath.Substring(1));
            if (path.EndsWith('/')) path += "index.html";
            return path;
        }
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
        public static string GetContentType(string path)
        {
            var contentTypes = new Dictionary<string, string>()
            {
                {"css","text/css"},
                {"txt","text/plain"},
                {"html","text/html"},
                {"js","text/javascript"},
                {"json","application/json"},
                {"gif","image/gif"},
                {"ico","image/vnd.microsoft.ico"},
                {"jpeg","image/jpeg"},
                {"jpg","image/jpeg"},
                {"png","image/png"},
                {"svg","image/svg+xml"}
            };
            string contentType;
            string file = path.Substring(path.LastIndexOf('/') + 1);
            string ext = string.Empty;
            if (file.Contains('.'))
            {
                ext = file.Substring(file.LastIndexOf('.') + 1);

            }
            contentTypes.TryGetValue(ext, out contentType);
            if (contentType == null) contentType = "application/octet-stream";
            return contentType;
        }
    }
}