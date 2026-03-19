using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SwiftPay.DTOs.RemittanceDTO;
using SwiftPay.Models;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repo;
        private readonly IMapper _mapper;

        public DocumentService(IDocumentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DocumentResponseDto> CreateAsync(CreateDocumentDto dto)
        {
            var entity = _mapper.Map<Document>(dto);
            entity.UploadedDate = System.DateTimeOffset.UtcNow;
            entity.CreatedDate = System.DateTime.UtcNow;
            entity.UpdateDate = System.DateTime.UtcNow;
            var created = await _repo.CreateAsync(entity);
            return _mapper.Map<DocumentResponseDto>(created);
        }

        public async Task<DocumentResponseDto?> GetByIdAsync(int documentId)
        {
            var entity = await _repo.GetByIdAsync(documentId);
            if (entity == null) return null;
            return _mapper.Map<DocumentResponseDto>(entity);
        }

        public async Task<List<DocumentResponseDto>> GetByRemitIdAsync(string remitId)
        {
            var list = await _repo.GetByRemitIdAsync(remitId);
            return _mapper.Map<List<DocumentResponseDto>>(list);
        }

        public async Task UpdateAsync(UpdateDocumentDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.DocumentId);
            if (entity == null) throw new System.Exception("Document not found.");

            entity.FileURI = dto.FileURI;
            if (!string.IsNullOrEmpty(dto.VerificationStatus))
                entity.VerificationStatus = (SwiftPay.Constants.Enums.VerificationStatus)System.Enum.Parse(typeof(SwiftPay.Constants.Enums.VerificationStatus), dto.VerificationStatus);

            entity.UpdateDate = System.DateTime.UtcNow;
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int documentId)
        {
            await _repo.DeleteAsync(documentId);
        }
    }
}
