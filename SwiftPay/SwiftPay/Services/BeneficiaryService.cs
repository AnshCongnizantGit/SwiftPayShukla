using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SwiftPay.Services.Interfaces;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Constants.Enums;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _repo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;

        public BeneficiaryService(IBeneficiaryRepository repo, ICustomerRepository customerRepo, IMapper mapper)
        {
            _repo = repo;
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<BeneficiaryResponseDto> CreateAsync(CreateBeneficiaryDto dto)
        {
            // Validate that Customer exists - BUSINESS LOGIC
            var customer = await _customerRepo.GetByIdAsync(dto.CustomerID);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {dto.CustomerID} does not exist.");

            // Use AutoMapper to map DTO to entity
            var entity = _mapper.Map<Beneficiary>(dto);

            var created = await _repo.CreateAsync(entity);
            return _mapper.Map<BeneficiaryResponseDto>(created);
        }

        public async Task<BeneficiaryResponseDto> GetByIdAsync(int beneficiaryId)
        {
            var beneficiary = await _repo.GetByIdAsync(beneficiaryId);
            return _mapper.Map<BeneficiaryResponseDto>(beneficiary);
        }

        public async Task<IEnumerable<BeneficiaryResponseDto>> GetByCustomerIdAsync(int customerId)
        {
            var beneficiaries = await _repo.GetByCustomerIdAsync(customerId);
            return _mapper.Map<List<BeneficiaryResponseDto>>(beneficiaries);
        }

        public async Task<IEnumerable<BeneficiaryResponseDto>> GetAllAsync()
        {
            var beneficiaries = await _repo.GetAllAsync();
            return _mapper.Map<List<BeneficiaryResponseDto>>(beneficiaries);
        }

        public async Task<BeneficiaryResponseDto> UpdateAsync(int beneficiaryId, UpdateBeneficiaryDto dto)
        {
            var beneficiary = await _repo.GetByIdAsync(beneficiaryId);
            if (beneficiary == null)
                throw new KeyNotFoundException($"Beneficiary with ID {beneficiaryId} not found.");

            // Use AutoMapper to map only non-null fields
            _mapper.Map(dto, beneficiary);

            var updated = await _repo.UpdateAsync(beneficiary);
            return _mapper.Map<BeneficiaryResponseDto>(updated);
        }

        public async Task<BeneficiaryResponseDto> UpdateVerificationStatusAsync(int beneficiaryId, UpdateBeneficiaryVerificationStatusDto dto)
        {
            var beneficiary = await _repo.GetByIdAsync(beneficiaryId);
            if (beneficiary == null)
                throw new KeyNotFoundException($"Beneficiary with ID {beneficiaryId} not found.");

            // Business logic: Update VerificationStatus
            beneficiary.VerificationStatus = dto.VerificationStatus;

            var updated = await _repo.UpdateAsync(beneficiary);
            return _mapper.Map<BeneficiaryResponseDto>(updated);
        }

        public async Task<bool> DeleteAsync(int beneficiaryId)
        {
            return await _repo.DeleteAsync(beneficiaryId);
        }
    }
}
