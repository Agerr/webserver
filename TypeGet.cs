using System.Net;
using System.Text;

namespace webserver
{
    public class TypeGet
    {
        public static string? path;
        public static async Task main(HttpListenerRequest request, HttpListenerResponse response, string rootDir, string catsDir)
        {
            path = Path.GetRequestedPath(request, rootDir);
            response.StatusCode = 200;

            if (!Path.Exists(path)) response.StatusCode = 404;

            switch(response.StatusCode)
            {
                case 200:
                    byte[] data = await System.IO.File.ReadAllBytesAsync(path);

                    response.StatusCode = response.StatusCode;
                    response.ContentType = Path.GetContentType(path);
                    response.ContentLength64 = data.LongLength;
                    response.ContentEncoding = Encoding.UTF8;

                    await response.OutputStream.WriteAsync(data, 0, data.Length);
                    break;
                default:
                    await Cat.Response(response, catsDir, response.StatusCode);
                    break;
            }
        }
    }
}