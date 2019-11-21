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
            string message;
            try
            {
                var result = await _contractUOW.ImportLeague(leagueCode.ToUpper());
                
                switch (result.Status)
                {

                    case STT.WebApi.Contract.Models.Import_LeagueResults.SuccessfullyImported:
                        message = "Successfully imported";
                        return StatusCode(201, message);
                    case STT.WebApi.Contract.Models.Import_LeagueResults.AlreadyImported:
                        message = "League already imported";
                        return StatusCode(409, message);
                    case STT.WebApi.Contract.Models.Import_LeagueResults.NotFound:
                        message = "Not found";
                        return StatusCode(404, message);
                    case STT.WebApi.Contract.Models.Import_LeagueResults.ServerError:
                        message = "Server Error";
                        return StatusCode(504, message);
                    default:
                        message = "Server Error";
                        return StatusCode(504, message);
                }
            }
            catch (Exception ex)
            {
                _contractUOW.CallRoolback();
                return StatusCode(500);
            }
            
        }

        [HttpGet]
        [Route("total-players/{leagueCode}")]
        [ProducesResponseType(typeof(void), 201)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> TotalPlayers(string leagueCode)
        {
            try
            {
                var result = await _contractUOW.TotalPlayesOnLeague(leagueCode.ToUpper());
                if (result.Success)
                {
                    string total = result.Players.ToString();
                    return Ok(total);
                }
                else
                {
                    return StatusCode(504, result.Message);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }
            
        }
    }
}