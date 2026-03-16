using System.Threading.Tasks;
using AutoMapper;
using SwiftPay.Services.Interfaces;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.DTOs.RemittanceDTO;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Services
{
    public class RemittanceService : IRemittanceService
    {
        private readonly IRemittanceRepository _repo;
        private readonly IMapper _mapper;

        public RemittanceService(IRemittanceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RemittanceRequest> CreateAsync(CreateRemittanceDto dto)
        {
            // Use AutoMapper to map DTO to entity
            var entity = _mapper.Map<RemittanceRequest>(dto);

            // set audit/default fields
            entity.CreatedDate = System.DateTime.UtcNow;
            entity.UpdateDate = System.DateTime.UtcNow;
            entity.IsDeleted = false;

            var created = await _repo.CreateAsync(entity);
            return created;
        }
    }
}
