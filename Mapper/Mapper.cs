using AutoMapper;
using WinnerGenerator_Backend.DTO;
using WinnerGenerator_Backend.Models;

namespace WinnerGenerator_Backend.Mapper;

public class MyMapper : Profile
{
    public MyMapper()
    {
        CreateMap<SubmitDataDTO, Winner>()
            .ForMember(dest => dest.GiveawayName, opt =>
                opt.MapFrom(src => src.giveawayName))
            .ForMember(dest => dest.Images, opt =>
                opt.MapFrom(src => src.images))
            .ForMember(dest => dest.ExcelFile, opt =>
                opt.MapFrom(src => src.base64Excel))
            .ForMember(dest => dest.IsActive, opt =>
                opt.MapFrom(src => true))
            .ForMember(dest => dest.TotalWinners, opt =>
                opt.MapFrom(src => src.totalWinners))
            .ForMember(dest => dest.InsertDateTime, opt =>
                opt.MapFrom(src => DateTime.Now)).ReverseMap();
    }
}