

using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using CariTakip.DataAccess.Context;
using CariTakip.DataAccess.Repositories.Interfaces;
using CariTakip.Entities;

namespace CariTakip.DataAccess.Repositories;

public class CariHareketRepository : ICariHareketRepository
{
    private readonly ApplicationDbContext _context;

    public CariHareketRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CariHareket>> GetAllAsync()
    {
        return await _context.CariHareketler.AsNoTracking()
        .OrderByDescending(x => x.Tarih)
        .ToListAsync();
    }

    public async Task<List <CariHareket>> GetByCariIdAsync(int cariId)
    {
        return await _context.CariHareketler.AsNoTracking()
        .Where(x => x.CariId == cariId)
        .OrderByDescending(x=>x.Tarih )
        .ToListAsync();
    }

    public async Task<CariHareket?> GetByIdAsync(int id)
    {
        return await _context.CariHareketler
        .FirstOrDefaultAsync(x=> x.Id == id);
    }

    public async Task  AddAsync(CariHareket hareket)
    {
        await _context.CariHareketler.AddAsync(hareket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CariHareket hareket)
    {
        _context.CariHareketler.Remove(hareket);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(CariHareket hareket)
    {
        _context.CariHareketler.Update(hareket);
        await _context.SaveChangesAsync();
    }
}