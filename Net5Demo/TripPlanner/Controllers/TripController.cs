using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace TripPlanner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        HttpClient _client = new HttpClient();
        private readonly ILogger<TripController> _logger;
        private readonly string _dataServiceUrl;

        public TripController(IConfiguration config, ILogger<TripController> logger)
        {
            _logger = logger;
            _dataServiceUrl = config["TripDataServiceUrl"];
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string[] addresses = new string[]{
                _dataServiceUrl+"/TripData?id=5",
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

            return new JsonResult(items);
        }
    }
}
