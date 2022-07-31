using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebApi.Helpers
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public string OldFileName { get; set; }
        public string FileExtension { get; set; }
        public CustomMultipartFormDataStreamProvider(string path) : base(path)
        { }
        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var extensions = new[] { "png", "gif", "jpg", "pdf", "doc", "docx", "xlsx", "xls" };
            var filename = headers.ContentDisposition.FileName
                .Replace("\"", string.Empty)
                .Replace("%", string.Empty)
                .Replace("&", string.Empty)
                .Replace("_", string.Empty);
            if (filename.IndexOf('.') < 0)
                throw new Exception("File doesn't have known extension.");
            var extension = filename.Split('.').Last();
            string[] parts = filename.Split('.');
            return parts.First() + "_" + Guid.NewGuid().ToString() + "." + parts.Last();
        }
    }
}