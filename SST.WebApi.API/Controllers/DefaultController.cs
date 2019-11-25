using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            ImportAnswer  answer;
            try
            {
                var result = await _contractUOW.ImportLeague(leagueCode.ToUpper());
                
                switch (result.Status)
                {

                    case STT.WebApi.Contract.Models.Import_LeagueResults.SuccessfullyImported:
                        answer = new ImportAnswer("Successfully imported");
                        return StatusCode(201, JsonConvert.SerializeObject(answer));
                    case STT.WebApi.Contract.Models.Import_LeagueResults.AlreadyImported:
                        answer = new ImportAnswer("League already imported");
                        return StatusCode(409, JsonConvert.SerializeObject(answer));
                    case STT.WebApi.Contract.Models.Import_LeagueResults.NotFound:
                        answer = new ImportAnswer("Not found");
                        return StatusCode(404, JsonConvert.SerializeObject(answer));
                    case STT.WebApi.Contract.Models.Import_LeagueResults.ServerError:
                        answer = new ImportAnswer("Server Error");
                        return StatusCode(504, JsonConvert.SerializeObject(answer));
                    default:
                        answer = new ImportAnswer("Server Error");
                        return StatusCode(504, JsonConvert.SerializeObject(answer));
                }
            }
            catch (Exception)
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
                AmmountAnswer answer;
                var result = await _contractUOW.TotalPlayesOnLeague(leagueCode.ToUpper());
                if (result.Success)
                {
                    answer = new AmmountAnswer(result.Players.ToString());
                    return Ok(answer);
                }
                else
                {
                    return StatusCode(504, result.Message);
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            
        }
    }
    public class ImportAnswer
    {
        public string message { get {
                return _message;
            } }
        private string _message { get; set; }
        public ImportAnswer(string message)
        {
            _message = message;
        }
    }
    public class AmmountAnswer
    {
        public string total
        {
            get
            {
                return _total;
            }
        }
        private string _total { get; set; }
        public AmmountAnswer(string total)
        {
            _total = total;
        }
    }
}