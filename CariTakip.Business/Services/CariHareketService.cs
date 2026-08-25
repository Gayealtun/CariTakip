
using CariTakip.Business.Dtos;
using CariTakip.DataAccess.Repositories.Interfaces;
using CariTakip.Business.Services.Interfaces;
using CariTakip.Entities;
using CariTakip.Entities.Enums;

namespace CariTakip.Business.Services;

public class CariHareketService : ICariHareketService
{
    private readonly ICariHareketRepository _cariHareketRepository;
    private readonly ICariRepository _cariRepository;

    public CariHareketService(
        ICariHareketRepository cariHareketRepository,
        ICariRepository cariRepository)
    {
        _cariHareketRepository = cariHareketRepository;
        _cariRepository = cariRepository;
    }

    public async Task<List<CariHareket>> GetAllAsync()
    {
        return await _cariHareketRepository.GetAllAsync();
    }

    public async Task<CariHareket?> GetByIdAsync(Guid id)
    {
        return await _cariHareketRepository.GetByIdAsync(id);
    }

    public async Task<List<CariHareket>> GetByCariIdAsync(Guid cariId)
    {
        Cari? cari = await _cariRepository.GetByIdAsync(cariId);

        if (cari == null)
        {
            throw new KeyNotFoundException("Cari bulunamadı.");
        }

        return await _cariHareketRepository.GetByCariIdAsync(cariId);
    }

    public async Task<CariHareket> CreateAsync(
        CreateCariHareketDto dto)
    {
        Cari? cari =
            await _cariRepository.GetByIdAsync(dto.CariId);

        if (cari == null)
        {
            throw new KeyNotFoundException("Cari bulunamadı.");
        }

        Validate(dto.Tutar, dto.Tip, dto.Kaynak);

        CariHareket hareket = new CariHareket
        {
            CariId = dto.CariId,
            Tarih = dto.Tarih ?? DateTime.UtcNow,
            Tip = dto.Tip,
            Aciklama = dto.Aciklama,
            Tutar = dto.Tutar,
            Kaynak = dto.Kaynak,
            KaynakId = dto.KaynakId,
            OlusturmaTarihi = DateTime.UtcNow
        };

        await _cariHareketRepository.AddAsync(hareket);

        return hareket;
    }

    public async Task<CariHareket> UpdateAsync(
        Guid id,
        UpdateCariHareketDto dto)
    {
        CariHareket? hareket =
            await _cariHareketRepository.GetByIdAsync(id);

        if (hareket == null)
        {
            throw new KeyNotFoundException(
                "Cari hareketi bulunamadı.");
        }

        Validate(dto.Tutar, dto.Tip, dto.Kaynak);

        hareket.Tarih = dto.Tarih ?? hareket.Tarih;
        hareket.Tip = dto.Tip;
        hareket.Aciklama = dto.Aciklama;
        hareket.Tutar = dto.Tutar;
        hareket.Kaynak = dto.Kaynak;
        hareket.KaynakId = dto.KaynakId;

        await _cariHareketRepository.UpdateAsync(hareket);

        return hareket;
    }

    public async Task DeleteAsync(Guid id)
    {
        CariHareket? hareket =
            await _cariHareketRepository.GetByIdAsync(id);

        if (hareket == null)
        {
            throw new KeyNotFoundException(
                "Cari hareketi bulunamadı.");
        }

        await _cariHareketRepository.DeleteAsync(hareket);
    }

    public async Task<decimal> GetBakiyeAsync(Guid cariId)
{
    var hareketler =
        await _cariHareketRepository.GetByCariIdAsync(cariId);

    decimal toplamBorc = hareketler
        .Where(h => h.Tip == HareketTuru.borc)
        .Sum(h => h.Tutar);

    decimal toplamAlacak = hareketler
        .Where(h => h.Tip == HareketTuru.alacak)
        .Sum(h => h.Tutar);

    return toplamBorc - toplamAlacak;
}

    private static void Validate(
        decimal tutar,
        HareketTuru tip,
        KaynakTuru kaynak)
    {
        if (tutar <= 0)
        {
            throw new ArgumentException(
                "Tutar sıfırdan büyük olmalıdır.");
        }

        if (!Enum.IsDefined(typeof(HareketTuru), tip))
        {
            throw new ArgumentException(
                "Geçersiz hareket türü.");
        }

        if (!Enum.IsDefined(typeof(KaynakTuru), kaynak))
        {
            throw new ArgumentException(
                "Geçersiz kaynak türü.");
        }
    }
}