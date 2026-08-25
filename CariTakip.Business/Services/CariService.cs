

using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using CariTakip.DataAccess.Context;
using CariTakip.DataAccess.Repositories;
using CariTakip.DataAccess.Repositories.Interfaces;
using CariTakip.Entities;
using CariTakip.Business.Services.Interfaces;

namespace CariTakip.Business.Services;

public class CariService : ICariService
{
    private readonly ICariRepository _cariRepository;

    public CariService (ICariRepository cariRepository)
    {
        _cariRepository = cariRepository;
    }
    public async Task<List<Cari> > GetAllAsync()
    {
        return await _cariRepository.GetAllAsync();
    }
    public async Task <Cari?>GetByIdAsync(Guid id)
    {
        return await _cariRepository.GetByIdAsync(id);
    }
    public async Task<Cari> CreateAsync(Cari cari)
    {
        if(string.IsNullOrWhiteSpace(cari.Unvan))
        {
            throw new ArgumentException("Unvan boş olamaz");
        }
        if(cari.KrediLimiti < 0)
        {
            throw new ArgumentException ("kredi limiti negatif olamaz");
        }
        if (!string.IsNullOrWhiteSpace(cari.VergiNoTC))
        {
        Cari? existingCari=await _cariRepository.GetByVergiNoAsync(cari.VergiNoTC);

        if(existingCari != null)
        {
            throw new InvalidOperationException("Bu vergi no kayitli");
        }
        
        }
    
        await _cariRepository.AddAsync(cari);

        return cari;
    }
    public async Task DeleteAsync(Guid id)
    {
        Cari? cari = await _cariRepository.GetByIdAsync(id);

        if(cari== null)
        {

        throw new InvalidOperationException("silinecek cari yok");
        }

        await _cariRepository.DeleteAsync(cari);
    }
    public async Task<Cari> UpdateAsync(Cari cari)
    {
       Cari? ExistingCari= await _cariRepository.GetByIdAsync(cari.Id);

       if(ExistingCari == null)
        {
            throw new InvalidOperationException("güncellenecek cari yok");
        }
        if (string.IsNullOrWhiteSpace(cari.Unvan))
        {
            throw new ArgumentException("Unvan boş olamaz");
        }
        if(cari.KrediLimiti < 0)
        {
            throw new ArgumentException("kredi limiti negatif olamaz");
        }
        await _cariRepository.UpdateAsync(cari);
        return cari;
    }
    
}