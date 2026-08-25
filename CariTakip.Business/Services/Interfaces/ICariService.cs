
using CariTakip.Entities;

namespace CariTakip.Business.Services.Interfaces;

public interface ICariService
{
    Task <List<Cari>>GetAllAsync();
    Task <Cari> CreateAsync(Cari cari);
    Task <Cari?>GetByIdAsync(Guid id);
    Task <Cari> UpdateAsync(Cari cari);
    Task DeleteAsync(Guid id);
}