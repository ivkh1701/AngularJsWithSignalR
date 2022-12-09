using AngularCrudWithSignalR.Factories;
using AngularCrudWithSignalR.Hubs;
using AngularCrudWithSignalR.Models;
using AngularCrudWithSignalR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AngularCrudWithSignalR.Controllers
{
    [ApiController]
    public class CustomerController : Controller
    {

        private readonly ICustomerfactory _customFactory;
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly IDownloadService _downloadService;

        public CustomerController(ICustomerfactory customFactory, IHubContext<SignalRHub> hubContext, IDownloadService downloadService)
        {
            _customFactory = customFactory;
            _hubContext = hubContext;
            _downloadService = downloadService;
        }


        [HttpPost]
        [Route("api/customer")]
        public async Task<IActionResult> AddCustomer([FromBody]CustomerModel model)
        {
            await _customFactory.AddCustomerAsync(model);
            await _hubContext.Clients.All.SendAsync("recived_response");
            return Ok();
        }

        [HttpGet]
        [Route("api/customer")]
        public async Task<IActionResult> GetCustomer()
        {
            var customers = await _customFactory.PrepareAllCustomer();
            return Ok(customers);
        }

        [HttpGet]
        [Route("api/customer/byid")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customers = await _customFactory.PrepareCustomerById(id);
            return Ok(customers);
        }

        [HttpDelete]
        [Route("api/customer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customFactory.DeleteCustomerAsync(id);
            await _hubContext.Clients.All.SendAsync("recived_response");
            return Ok();
        }

        [HttpPut]
        [Route("api/customer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel model)
        {
            await _customFactory.UpdateCustomerAsync(model);
            await _hubContext.Clients.All.SendAsync("recived_response");
            return Ok();
        }

        [Route("api/downloadfile")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var download = await _downloadService.GetDownloadAsync(id);
            return new FileContentResult(download.BinaryData, download.Mimetype)
            {
                FileDownloadName = download.FileName
            };
        }

    }
}
