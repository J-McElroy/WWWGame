using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WWWGame.LogicLayer.Parser;

namespace WWWGame.SourceParser
{
    public class ContentLoader : IContentLoader
    {
        public Task<string> GetXml(string url)
        {
            var tcs = new TaskCompletionSource<string>();
            var web = new WebClient();

            web.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null) tcs.TrySetException(e.Error);
                else if (e.Cancelled) tcs.TrySetCanceled();
                else tcs.TrySetResult(e.Result);
            };

            web.DownloadStringAsync(new Uri(url + "\\xml"));
            return tcs.Task;
        }

        public async Task<byte[]> GetFile(string url)
        {
            var tcs = new TaskCompletionSource<byte[]>();
            WebClient client = new WebClient();
            client.OpenReadCompleted += (s, e) =>
            {
                if (e.Error != null) tcs.TrySetException(e.Error);
                else if (e.Cancelled) tcs.TrySetCanceled();
                else
                {
                    Stream imageStream = e.Result;
                    var result = new byte[imageStream.Length];
                    imageStream.ReadAsync(result, 0, (int)imageStream.Length);
                    tcs.TrySetResult(result);
                }
                
            };
            client.OpenReadAsync(new Uri(url));
            return await tcs.Task;
        }

        //public string GetXml(string url)
        //{
        //    string xmlStr;
        //    using (var wc = new WebClient())
        //    {
        //        wc.Encoding = Encoding.UTF8;
        //        xmlStr = wc.DownloadStringAsync(url + "\\xml");
        //    }
        //    return xmlStr;
        //}
    }
}
