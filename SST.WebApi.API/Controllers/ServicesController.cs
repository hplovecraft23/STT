using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using STT.WebApi.Contract.Interfaces;

namespace SST.WebApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        
        IContractUOW _contractUOW;
        public ServicesController(IContractUOW contractUOW)
        {
            _contractUOW = contractUOW;
        }

        [HttpGet]
        [Route("GetLeages")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(void), 504)]
        public async Task<IActionResult> GetLeages()
        {
            var result = await _contractUOW.GetLeagues();
            _contractUOW.CompetitionListCache = result;
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                string message = "Server Error";
                return StatusCode(504, message);
            }
        }

        [HttpPost]
        [Route("ChangeAPIKEY/{newkey}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(void), 504)]
        public IActionResult ChangeAPIKEY(string newkey)
        {
            try
            {
                _contractUOW.ChangeAPIKey(newkey);
                return Ok();
            }
            catch (Exception)
            {
                string message = "Server Error";
                return StatusCode(504, message);
            }
        }
    }
}