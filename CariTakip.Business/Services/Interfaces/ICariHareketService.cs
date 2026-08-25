using CariTakip.Business.Dtos;
using CariTakip.Entities;
using CariTakip.DataAccess.Repositories.Interfaces;

namespace CariTakip.Business.Services.Interfaces;

public interface ICariHareketService
{
    Task<List<CariHareket>> GetAllAsync();

    Task<CariHareket?> GetByIdAsync(Guid id);

    Task<List<CariHareket>> GetByCariIdAsync(Guid cariId);

    Task<CariHareket> CreateAsync(
        CreateCariHareketDto dto);

    Task<CariHareket> UpdateAsync(
        Guid id,
        UpdateCariHareketDto dto);

    Task DeleteAsync(Guid id);
    Task<decimal> GetBakiyeAsync(Guid cariId);
}