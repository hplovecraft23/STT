using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using STT.WebApi.APIClient.Models;

namespace STT.WebApi.APIClient.Logic
{
    public class AutomapperConfig
    {
        public MapperConfiguration CompetitionListMapConfig;
        public MapperConfiguration TeamsCompetitionListMapConfig;
        public MapperConfiguration GetTeamConfig;
        public AutomapperConfig()
        {
            CompetitionListMapConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<CompetitionListJSON, CompetitionList>();
                c.CreateMap<CompetitionJSON, Competition>();
            });
            TeamsCompetitionListMapConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<TeamCompetitionsJSON, CompetitionTeamList>();
                c.CreateMap<CompetitionJSON, Competition>();
                c.CreateMap<TeamJSON, TeamsCompetitionTeamDTO>().ForMember(x => x.areaName, x => x.MapFrom(x => x.area.name));
                
            });
            GetTeamConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<GETTeamJSON, Team>()
                .ForMember(x => x.areaName, x => x.MapFrom(x => x.area.name));
                c.CreateMap<Squad, Player>();

            });

            CompetitionListMapConfig.AssertConfigurationIsValid();
            TeamsCompetitionListMapConfig.AssertConfigurationIsValid();
            GetTeamConfig.AssertConfigurationIsValid();
        }
    }
}
