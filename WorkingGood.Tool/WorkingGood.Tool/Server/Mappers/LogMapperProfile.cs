using System;
using AutoMapper;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Shared.Logs;

namespace WorkingGood.Tool.Server.Mappers
{
	public class LogMapperProfile : Profile
    {
        public LogMapperProfile()
        {
            CreateMap<LogData, LogDto>();
        }
    }
}

