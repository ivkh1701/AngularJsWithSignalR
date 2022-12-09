using AngularCrudWithSignalR.Data.Entites;
using AngularCrudWithSignalR.Services;
using Microsoft.AspNetCore.Mvc;

namespace AngularCrudWithSignalR.Controllers
{
    public class DownloadController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerService _customFactory;
        private readonly IDownloadService _downloadService;


        public DownloadController(ILogger<HomeController> logger,
            ICustomerService customFactory, IDownloadService downloadService)
        {
            _logger = logger;
            _customFactory = customFactory;
            _downloadService = downloadService;
        }


        [Route("download/uploadFiles")]
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync()
        {
            var httpPostedFile = Request.Form.Files.FirstOrDefault();

            if (httpPostedFile is null)
                return Json(new { pictureId = 0 });


            var contentType = httpPostedFile.ContentType;
            var filename = httpPostedFile.FileName;
            var binary = await _downloadService.GetDownloadBitsAsync(httpPostedFile);

            var download = new Download
            {
                BinaryData = binary,
                Mimetype = contentType,
                FileName = filename
            };

            await _downloadService.InsertDownloadAsync(download);

            return Json(new { pictureId = download.Id });
        }
    }
}
