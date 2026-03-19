using System.Threading.Tasks;
using AutoMapper;
using SwiftPay.Constants.Enums;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.DTOs.RemittanceDTO;
using SwiftPay.Models;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;


namespace SwiftPay.Services
{
	public class RemittanceService : IRemittanceService
	{
        private readonly IRemittanceRepository _repo;
        private readonly IRemitValidationRepository _validationRepo;
        private readonly IMapper _mapper;

        public RemittanceService(IRemittanceRepository repo, IRemitValidationRepository validationRepo, IMapper mapper)
        {
            _repo = repo;
            _validationRepo = validationRepo;
            _mapper = mapper;
        }

		/// <summary>
		/// Creates a remittance in Draft state.
		/// </summary>
		public async Task<CreateRemittanceResponseDto> CreateAsync(CreateRemittanceDto dto)
		{
			// Map input DTO → domain entity
			var entity = _mapper.Map<RemittanceRequest>(dto);

			// Persist entity
			var createdEntity = await _repo.CreateAsync(entity);

			// Map entity → response DTO
			return _mapper.Map<CreateRemittanceResponseDto>(createdEntity);
		}

        /// <summary>
        /// Gets remittance by ID (string RemitId).
        /// </summary>
        public async Task<CreateRemittanceResponseDto?> GetByIdAsync(string remitId)
        {
            var entity = await _repo.GetByIdAsync(remitId);

            if (entity == null)
                return null;

            return _mapper.Map<CreateRemittanceResponseDto>(entity);
        }


        public async Task<ValidateRemittanceResponseDto> ValidateAsync(string remitId)
        {
            if (string.IsNullOrWhiteSpace(remitId))
                throw new ArgumentException("Invalid remittance ID.");

            // Fetch remittance
            var remittance = await _repo.GetByIdAsync(remitId);
            if (remittance == null)
                throw new Exception("Remittance not found.");

            if (remittance.Status != RemittanceRequestStatus.Draft)
                throw new Exception("Only draft remittances can be validated.");

            var validations = new List<RemitValidation>();

            // Corridor rule (example)
            validations.Add(new RemitValidation
            {
                ValidationId = Guid.NewGuid(),
                RemitId = remittance.RemitId,
                RuleName = ValidationRuleName.Corridor,
                Result = ValidationResult.Pass,
                Message = string.Empty,
                CheckedDate = DateTimeOffset.UtcNow
            });

            // Limit rule (example – real logic later)
            validations.Add(new RemitValidation
            {
                ValidationId = Guid.NewGuid(),
                RemitId = remittance.RemitId,
                RuleName = ValidationRuleName.Limit,
                Result = ValidationResult.Pass,
                Message = string.Empty,
                CheckedDate = DateTimeOffset.UtcNow
            });

            // Docs rule example (FAIL case)
            bool hasInvoice = remittance.Documents != null && remittance.Documents.Any(d => d.DocType == DocumentType.Invoice);

            if (!hasInvoice)
            {
                validations.Add(new RemitValidation
                {
                    ValidationId = Guid.NewGuid(),
                    RemitId = remittance.RemitId,
                    RuleName = ValidationRuleName.Docs,
                    Result = ValidationResult.Fail,
                    Message = "Invoice document is required.",
                    CheckedDate = DateTimeOffset.UtcNow
                });
            }
            else
            {
                validations.Add(new RemitValidation
                {
                    ValidationId = Guid.NewGuid(),
                    RemitId = remittance.RemitId,
                    RuleName = ValidationRuleName.Docs,
                    Result = ValidationResult.Pass,
                    Message = string.Empty,
                    CheckedDate = DateTimeOffset.UtcNow
                });
            }

            // Save validation records via validation repository
            await _validationRepo.AddRangeAsync(validations);

            // Determine overall result
            bool hasFailure = validations.Any(v => v.Result == ValidationResult.Fail);

            remittance.Status = hasFailure
                ? RemittanceRequestStatus.Draft
                : RemittanceRequestStatus.Validated;

            await _repo.UpdateAsync(remittance);

            // Map validation entities -> DTOs using AutoMapper
            var validationDtos = _mapper.Map<List<RemitValidationDto>>(validations);

            // Build response using AutoMapper-mapped validation DTOs
            return new ValidateRemittanceResponseDto
            {
                RemitId = remittance.RemitId,
                Status = remittance.Status.ToString(),
                Validations = validationDtos
            };
        }


        public async Task UpdateAsync(string remitId, CreateRemittanceDto dto)
        {
            if (string.IsNullOrWhiteSpace(remitId))
                throw new ArgumentException("Invalid remittance ID.");

            var remittance = await _repo.GetByIdAsync(remitId);
            if (remittance == null)
                throw new Exception("Remittance not found.");

            // Update allowed fields
            remittance.CustomerId = dto.CustomerId;
            remittance.BeneficiaryId = dto.BeneficiaryId;
            remittance.FromCurrency = dto.FromCurrency;
            remittance.ToCurrency = dto.ToCurrency;
            remittance.SendAmount = dto.SendAmount;
            remittance.UpdateDate = DateTime.UtcNow;

            await _repo.UpdateAsync(remittance);
        }

        public async Task<bool> DeleteAsync(string remitId)
        {
            if (string.IsNullOrWhiteSpace(remitId))
                throw new ArgumentException("Invalid remittance ID.");

            var remittance = await _repo.GetByIdAsync(remitId);
            if (remittance == null)
                throw new Exception("Remittance not found.");

            // Soft delete
            remittance.IsDeleted = true;
            remittance.UpdateDate = DateTime.UtcNow;
            await _repo.UpdateAsync(remittance);
            return true;
        }

        public async Task UpdateValidationAsync(RemitValidationDto dto)
        {
            var existing = await _validationRepo.GetByIdAsync(dto.ValidationId);
            if (existing == null)
                throw new Exception("Validation record not found.");

            // Ensure remit id matches
            if (!string.Equals(existing.RemitId, dto.RemitId, StringComparison.OrdinalIgnoreCase))
                throw new Exception("RemitId mismatch.");

            // update fields
            existing.RuleName = Enum.TryParse<ValidationRuleName>(dto.Rule, out var rn) ? rn : existing.RuleName;
            existing.Result = Enum.TryParse<ValidationResult>(dto.Result, out var res) ? res : existing.Result;
            existing.Message = dto.Message ?? string.Empty;
            existing.CheckedDate = dto.CheckedDate;
            existing.UpdateDate = DateTime.UtcNow;

            await _validationRepo.UpdateAsync(existing);
        }

        public async Task DeleteValidationAsync(Guid validationId)
        {
            await _validationRepo.DeleteAsync(validationId);
        }



        public async Task<List<RemitValidationDto>> GetValidationsAsync(string remitId)
        {
            if (string.IsNullOrWhiteSpace(remitId))
                throw new ArgumentException("Invalid remittance ID.");

            var validations = await _validationRepo.GetByRemitIdAsync(remitId);

            if (validations == null || validations.Count == 0)
                return new List<RemitValidationDto>();


            // Map using AutoMapper
            return _mapper.Map<List<RemitValidationDto>>(validations);
        }
	}
}