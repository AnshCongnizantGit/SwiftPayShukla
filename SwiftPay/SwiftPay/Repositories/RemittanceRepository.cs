using System.Threading.Tasks;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.Configuration;

namespace SwiftPay.Repositories
{
    public class RemittanceRepository : IRemittanceRepository
    {
        private readonly AppDbContext _db;

        public RemittanceRepository(AppDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Asynchronously creates a new remittance request and persists it to the database.
        /// </summary>
        /// <remarks>This method adds the specified entity to the database context and commits the
        /// changes. Ensure that the entity is properly initialized before calling this method.</remarks>
        /// <param name="entity">The remittance request entity to add. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created remittance request
        /// entity, including any generated values.</returns>
        public async Task<RemittanceRequest> CreateAsync(RemittanceRequest entity)
        {
            await _db.Set<RemittanceRequest>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
