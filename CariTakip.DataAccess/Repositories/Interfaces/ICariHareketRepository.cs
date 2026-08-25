using CariTakip.Entities;

namespace CariTakip.DataAccess.Repositories.Interfaces;

public interface ICariHareketRepository
{
    Task<List<CariHareket>>GetAllAsync();
    Task<CariHareket?>GetByIdAsync(Guid id);
    Task <List<CariHareket>>GetByCariIdAsync(Guid cariId);
    Task AddAsync(CariHareket hareket);
    Task DeleteAsync(CariHareket hareket);
    Task UpdateAsync(CariHareket hareket);
}