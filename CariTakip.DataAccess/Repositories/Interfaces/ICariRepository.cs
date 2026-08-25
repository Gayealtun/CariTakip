
using CariTakip.Entities;
namespace CariTakip.DataAccess.Repositories.Interfaces;

public interface ICariRepository{ 
Task <List<Cari>> GetAllAsync();
Task <Cari?> GetByIdAsync (Guid id);
 Task <Cari?> GetByVergiNoAsync(string VergiNo);
Task AddAsync (Cari cari);
Task UpdateAsync (Cari cari);
Task DeleteAsync (Cari cari);
}