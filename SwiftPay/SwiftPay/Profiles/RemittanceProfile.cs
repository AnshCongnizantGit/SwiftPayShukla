using AutoMapper;
using SwiftPay.DTOs.RemittanceDTO;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Profiles
{
    public class RemittanceProfile : Profile
    {
        public RemittanceProfile()
        {
            CreateMap<CreateRemittanceDto, RemittanceRequest>()
                .ForMember(dest => dest.RemitId, opt => opt.Ignore());
        }
    }
}
