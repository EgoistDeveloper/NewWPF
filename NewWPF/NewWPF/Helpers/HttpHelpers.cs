using System;
using System.IO;
using System.Net;

namespace NewWPF.Helpers
{
    public static class HttpHelpers
    {
        public static string Get(this string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public static bool Download(this string uri, string fileName)
        {
            using var client = new WebClient();
            client.DownloadFileAsync(new Uri(uri), fileName);

            return File.Exists(fileName);
        }
    }
}