using AutoMapper;
using WorkingGood.Tool.Domain.Enums;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Shared.Settings;

namespace WorkingGood.Tool.Server.Mappers;

public class EmailTemplatesMapperProfile : Profile
{
    public EmailTemplatesMapperProfile()
    {
        CreateMap<EmailTemplate, EmailTemplateVM>();
        CreateMap<EmailTemplateDto, EmailTemplate>()
            .ForMember(dest => dest.Destination,
                opt => opt.MapFrom(
                    src => Enum.GetValues<EmailTemplateDestination>().FirstOrDefault(x => x.ToString() == src.Destination)));
    }
}