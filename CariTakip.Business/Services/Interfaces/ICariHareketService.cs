using CariTakip.Business.Dtos;
using CariTakip.Entities;
using CariTakip.DataAccess.Repositories.Interfaces;

namespace CariTakip.Business.Services.Interfaces;

public interface ICariHareketService
{
    Task<List<CariHareket>> GetAllAsync();

    Task<CariHareket?> GetByIdAsync(int id);

    Task<List<CariHareket>> GetByCariIdAsync(int cariId);

    Task<CariHareket> CreateAsync(
        CreateCariHareketDto dto);

    Task<CariHareket> UpdateAsync(
        int id,
        UpdateCariHareketDto dto);

    Task DeleteAsync(int id);
}