using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaperNestTest.Models;
using PaperNestTest.Services;
using Microsoft.AspNetCore.Http;

namespace PaperNestTest.Controllers
{
    /// <summary>
    /// Controller related with network coverage.
    /// https://localhost:5001/networkcoverage/
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class NetworkCoverageController : ControllerBase
    {

        private INetworkCoverageService _networkCoverageService;

        public NetworkCoverageController(INetworkCoverageService networkCoverageService)
        {
            _networkCoverageService = networkCoverageService;
        }

        /// <summary>
        /// HTTP GET Request handler.
        /// It gives the network coverage of the address sent.
        /// Example : https://localhost:5001/networkcoverage/?q=42+rue+papernest+75011+Paris
        /// </summary>
        /// <param name="q">Address</param>
        /// <returns>List of provider and their coverage og 2G/3G/4G around the given address.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProviderCoverage))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ProviderCoverage>> Get(string q)
        {
            Console.WriteLine("GET Request catched.");
            if (q == null)
            {
                Console.WriteLine("Empty address.");
                var message = string.Format("Address cannot be empty.");
                Console.WriteLine("Request processed. " + message);
                return BadRequest(message);
            }

            Console.WriteLine("Request with params : " + q);

            IEnumerable<ProviderCoverage> providerCoverageList = _networkCoverageService.GetByAddress(q);
            if (providerCoverageList.Count() == 0)
            {
                var message = string.Format("No providers data found for address = {0}.", q);
                Console.WriteLine("Request processed. " + message);
                return NotFound(message);
            }
            if (providerCoverageList == null)
            {
                var message = string.Format("Address not found. Address = {0}.", q);
                Console.WriteLine("Request processed. " + message);
                return NotFound(message);
            }


            Console.WriteLine("Request processed.");
            return Ok(providerCoverageList);
        }
    }
}
