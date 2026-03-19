using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.Models;

namespace SwiftPay.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document> CreateAsync(Document doc);
        Task<Document?> GetByIdAsync(int documentId);
        Task<List<Document>> GetByRemitIdAsync(string remitId);
        Task UpdateAsync(Document doc);
        Task DeleteAsync(int documentId);
    }
}
