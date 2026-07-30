
using TaskTracking.Entities;

public interface ICariService
{
    Task <List<Cari>>GetAllAsync();
    Task <Cari> CreateAsync(Cari cari);
    Task <Cari?>GetByIdAsync(int id);
    Task <Cari> UpdateAsync(Cari cari);
    Task DeleteAsync(int id);
}