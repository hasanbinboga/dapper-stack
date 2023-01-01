using AutoMapper;
using NetFrame.Core.Entities;

namespace NetFrame.Core.Dtos
{
    public class CoreDtoMapProfile: Profile
    {
        public CoreDtoMapProfile()
        {
            CreateMap<OptionEntity, OptionDto>().ReverseMap();
        }
    }
}
