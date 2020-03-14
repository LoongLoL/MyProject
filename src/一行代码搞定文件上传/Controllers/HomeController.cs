using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using 一行代码搞定文件上传.Filters;
using 一行代码搞定文件上传.Utilities;

namespace 一行代码搞定文件上传.Controllers
{
    [Route("api/home")]
    public class HomeController : Controller
    {
        private readonly long _fileSizeLimit;//上传文件的大小限制
        private readonly string[] _permittedExtensions = { ".txt", ".jpg" };//允许的文件后缀名
        private readonly string _targetFilePath;//文件保存的目标文件夹路径
        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        public HomeController(IConfiguration config)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
        }


        #region snippet_UploadPhysical
        [HttpPost, Route("upload")]
        [DisableFormValueModelBinding]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPhysical()
        {

            //if (!fileMsg.UserId.HasValue || string.IsNullOrWhiteSpace(fileMsg.FileFloder))
            //{
            //    ModelState.AddModelError("File", $"上传文件缺少关键信息！");
            //    return BadRequest(ModelState);
            //}

            //if (string.IsNullOrWhiteSpace(fileMsg))
            //{
            //    ModelState.AddModelError("File", $"上传文件缺少关键信息！");
            //    return BadRequest(ModelState);
            //}

            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                ModelState.AddModelError("File",
                    $"The request couldn't be processed (Error 1).");
                // Log error

                return BadRequest(ModelState);
            }

            var boundary = MultipartRequestHelper.GetBoundary(
               MediaTypeHeaderValue.Parse(Request.ContentType),
               _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // This check assumes that there's a file
                    // present without form data. If form data
                    // is present, this method immediately fails
                    // and returns the model error.
                    if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition))
                    {
                        ModelState.AddModelError("File",
                            $"The request couldn't be processed (Error 2).");
                        // Log error

                        return BadRequest(ModelState);
                    }
                    else
                    {
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = Path.GetRandomFileName();//这里获取的是随机文件名

                        trustedFileNameForFileStorage = trustedFileNameForFileStorage + Path.GetExtension(trustedFileNameForDisplay);

                        // **WARNING!**
                        // In the following example, the file is saved without
                        // scanning the file's contents. In most production
                        // scenarios, an anti-virus/anti-malware scanner API
                        // is used on the file before making the file available
                        // for download or for use by other systems. 
                        // For more information, see the topic that accompanies 
                        // this sample.

                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                            section, contentDisposition, ModelState,
                            _permittedExtensions, _fileSizeLimit);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        using (var targetStream = System.IO.File.Create(
                            Path.Combine(_targetFilePath, trustedFileNameForFileStorage)))
                        {
                            await targetStream.WriteAsync(streamedFileContent);


                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            return Created(nameof(HomeController), null);
        }
        #endregion

        #region snippet_UploadPhysical

        [HttpPost, Route("uploadwithmsg")]
        public async Task<IActionResult> UploadPhysical(FileMsg fileMsg)
        {
            if (!fileMsg.UserId.HasValue || string.IsNullOrWhiteSpace(fileMsg.FileGroup))
            {
                ModelState.AddModelError("File", $"上传文件缺少关键信息！");
                return BadRequest(ModelState);
            }

            if (fileMsg.FileList == null || fileMsg.FileList.Count == 0)
            {
                ModelState.AddModelError("File", $"没有发现上传的文件！");
                return BadRequest(ModelState);
            }


            foreach (var section in fileMsg.FileList)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // This check assumes that there's a file
                    // present without form data. If form data
                    // is present, this method immediately fails
                    // and returns the model error.
                    if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition))
                    {
                        ModelState.AddModelError("File",
                            $"The request couldn't be processed (Error 2).");
                        // Log error

                        return BadRequest(ModelState);
                    }
                    else
                    {
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                            contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = Path.GetRandomFileName(); //这里获取的是随机文件名

                        trustedFileNameForFileStorage =
                            trustedFileNameForFileStorage + Path.GetExtension(trustedFileNameForDisplay);

                        // **WARNING!**
                        // In the following example, the file is saved without
                        // scanning the file's contents. In most production
                        // scenarios, an anti-virus/anti-malware scanner API
                        // is used on the file before making the file available
                        // for download or for use by other systems. 
                        // For more information, see the topic that accompanies 
                        // this sample.

                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                            section, contentDisposition, ModelState,
                            _permittedExtensions, _fileSizeLimit);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        using (var targetStream = System.IO.File.Create(
                            Path.Combine(_targetFilePath, trustedFileNameForFileStorage)))
                        {
                            await targetStream.WriteAsync(streamedFileContent);


                        }
                    }


                }

            }

            return Created(nameof(HomeController), null);
        }
        #endregion

        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader =
                MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            // UTF-7 is insecure and shouldn't be honored. UTF-8 succeeds in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }

            return mediaType.Encoding;
        }

    }

    public class FileMsg
    {
        public int? UserId { get; set; }
        public string FileGroup { get; set; }
        public List<IFormFile> FileList { get; set; }
    }
}