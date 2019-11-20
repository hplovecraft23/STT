using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using STT.WebApi.Contract.Interfaces;

namespace SST.WebApi.API.Controllers
{
    [ApiController]
    [Route("")]
    public class DefaultController : ControllerBase
    {
        IContractUOW _contractUOW;

        public DefaultController(IContractUOW contractUOW)
        {
            _contractUOW = contractUOW;
        }

        [HttpGet]
        [Route("import-league/{leagueCode}")]
        [ProducesResponseType(typeof(void), 201)]
        [ProducesResponseType(typeof(void), 409)]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(void), 504)]
        public async Task<IActionResult> ImportLeague(string leagueCode)
        {
            var result = await _contractUOW.ImportLeague(leagueCode);
            if (result.Status == STT.WebApi.Contract.Models.Import_LeagueResults.SuccessfullyImported)
            {
                return Ok(result);
            }
            else
            {
                string message = "Server Error";
                return StatusCode(504, message);
            }
        }

        [HttpGet]
        [Route("total-players/{leagueCode}")]
        [ProducesResponseType(typeof(void), 201)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> TotalPlayers(string leagueCode)
        {
            return Ok();
        }
    }
}