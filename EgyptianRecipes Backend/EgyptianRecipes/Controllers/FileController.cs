using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ViewModels;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    public class FileController : ApiController
    {

        [HttpPost]
        public async Task<ResultViewModel<List<UploadedFile>>> Upload()
        {
            ResultViewModel<List<UploadedFile>> resultViewModel = new ResultViewModel<List<UploadedFile>>();
            List<UploadedFile> uploadedFiles = new List<UploadedFile>();
            try
            {
                if (Request.Content.IsMimeMultipartContent())
                {
                    string root = HttpContext.Current.Server.MapPath("~/Uploads");
                    if (!Directory.Exists(root))
                        Directory.CreateDirectory(root);

                    CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(root);
                    await Request.Content.ReadAsMultipartAsync(provider);
                    string domain = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                    foreach (MultipartFileData file in provider.FileData)
                        uploadedFiles.Add(new UploadedFile
                        {
                            Name = file.LocalFileName.Split('\\').Last().Split('.').First().Split('_').First(),
                            Path = file.LocalFileName.Split('\\').Last(),
                            Extension = file.LocalFileName.Split('\\').Last().Split('.').Last()
                        });
                    resultViewModel.Data = uploadedFiles;
                }
                else
                {
                    resultViewModel.Successed = false;
                    resultViewModel.Message = "حدث خطأ ما";
                }
            }
            catch (Exception ex)
            {
                resultViewModel.Successed = false;
                resultViewModel.Message = "Error";
            }
            return resultViewModel;
        }
    }
}
