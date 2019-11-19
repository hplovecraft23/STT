using System;
using System.Collections.Generic;
using System.Text;

namespace STT.WebApi.Contract.Models
{
    public enum Import_LeagueResults
    {
        SuccessfullyImported,
        AlreadyImported,
        NotFound,
        ServerError
    }
}
