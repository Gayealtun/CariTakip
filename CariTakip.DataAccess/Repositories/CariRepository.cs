using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SQLitePCL;
using CariTakip.DataAccess.Context;
using CariTakip.DataAccess.Repositories.Interfaces;
using CariTakip.Entities;

namespace CariTakip.DataAccess.Repositories;

public class CariRepository : ICariRepository
{
    private readonly ApplicationDbContext _context;

    public CariRepository (ApplicationDbContext context)
    {
       _context=context ;
    }


public async Task<List<Cari>> GetAllAsync()
    {
        return await _context.Cariler.OrderBy(s=>s.OlusturmaTarihi).ToListAsync();
    } 
public async Task<Cari?> GetByIdAsync(Guid id)
    {
        return await _context.Cariler.FindAsync(id);
    }    

public async Task AddAsync(Cari cari)
    {
        await _context.Cariler.AddAsync(cari);
        await _context.SaveChangesAsync();
    }     
public async Task UpdateAsync(Cari cari)
    {
        _context.Cariler.Update(cari);
        await _context.SaveChangesAsync(); 
    }
public async Task DeleteAsync(Cari cari)
    {
        _context.Cariler.Remove(cari);
        await _context.SaveChangesAsync();
    }  
public async Task<Cari?> GetByVergiNoAsync(string VergiNo)
    {
        return await _context.Cariler.FirstOrDefaultAsync(c => c.VergiNoTC == VergiNo);
    }   
}   