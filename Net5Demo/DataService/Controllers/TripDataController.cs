using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataService.Services;
using Microsoft.Extensions.Configuration;

namespace DataService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripDataController : ControllerBase
    {
        HttpClient _client = new HttpClient();
        private readonly ILogger<TripDataController> _logger;
        private readonly IDataService _service;
        private readonly string _userServiceUrl;
        public TripDataController(IDataService service, ILogger<TripDataController> logger, IConfiguration config)
        {
            _logger = logger;
            _service = service;
            _userServiceUrl = config["UserServiceUrl"];
            _logger.LogInformation("Setting User-Service URL:"+_userServiceUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            string[] addresses = new string[]{
                _userServiceUrl+"/user?id=34237874-2feddfd-232",
            };

            List<string> items = new List<string>();
            foreach(string address in addresses)
            {
                try
                {
                    var response = await _client.GetAsync(address).ConfigureAwait(false);
                    items.Add(response.StatusCode.ToString());
                }
                catch(Exception e)
                {
                    _logger.LogWarning("Couldn't request from '"+address+"': "+e.Message);
                }
            }

            var names = await _service.GetDataForTrip(id);
            foreach(string name in names)
            {
                items.Add(name);
            }
            return new JsonResult(names);
        }
    }
}
