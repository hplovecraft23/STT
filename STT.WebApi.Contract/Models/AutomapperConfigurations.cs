﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using STT.WebApi.APIClient.Models;

namespace STT.WebApi.Contract.Models
{
    public class AutomapperConfigurations
    {
        public MapperConfiguration CompetitionAPItoLocal;
        public MapperConfiguration TeamAPItoLocal;
        public AutomapperConfigurations()
        {
            CompetitionAPItoLocal = new MapperConfiguration(c =>
            {
                c.CreateMap<Competition, Data.Models.Competition>();
            });
            TeamAPItoLocal = new MapperConfiguration(c =>
            {
                c.CreateMap<Team, Data.Models.Team>();
            });
            
            CompetitionAPItoLocal.AssertConfigurationIsValid();
            TeamAPItoLocal.AssertConfigurationIsValid();
        }
    }
}