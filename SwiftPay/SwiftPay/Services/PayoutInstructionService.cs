using AutoMapper;
using SwiftPay.Constants.Enums;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.DTOs.PayoutDTO;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Services
{
    public class PayoutInstructionService : IPayoutInstructionService
    {
        private readonly IPayoutInstructionRepository _repo;
        private readonly IMapper _mapper;

        public PayoutInstructionService(IPayoutInstructionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PayoutInstructionResponseDto> CreateInstructionAsync(CreatePayoutInstructionDto dto)
        {
            var entity = _mapper.Map<PayoutInstruction>(dto);

            entity.InstructionId = Guid.NewGuid().ToString();
            entity.SentDate = DateTimeOffset.UtcNow;
            entity.CreatedDate = DateTimeOffset.UtcNow;
            entity.UpdateDate = DateTimeOffset.UtcNow;
            entity.IsDeleted = false;

            await _repo.AddAsync(entity);
            return _mapper.Map<PayoutInstructionResponseDto>(entity);
        }

        public async Task<PayoutInstructionResponseDto?> GetByIdAsync(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<PayoutInstructionResponseDto>(entity);
        }

        public async Task<IEnumerable<PayoutInstructionResponseDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PayoutInstructionResponseDto>>(entities);
        }

        public async Task<bool> UpdateAsync(string id, CreatePayoutInstructionDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            // Maps DTO values onto the existing entity
            _mapper.Map(dto, existing);

            existing.UpdateDate = DateTimeOffset.UtcNow;

            await _repo.UpdateAsync(existing);
            return true;
        }
        public async Task<bool> UpdateStatusAsync(string id, PayOutInstructionStatus status, string ackRef)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.PartnerStatus = status;
            existing.AckRef = ackRef;
            existing.UpdateDate = DateTimeOffset.UtcNow;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}