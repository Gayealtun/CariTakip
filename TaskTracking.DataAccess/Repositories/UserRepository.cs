using Microsoft.EntityFrameworkCore;
using TaskTracking.DataAccess.Context;
using TaskTracking.DataAccess.Repositories.Interfaces;
using TaskTracking.Entities.Models;

namespace TaskTracking.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;//readonly güvenlik için 
    public UserRepository (ApplicationDbContext context) //constructor,DI devrede 
    {
        _context=context; //applicationdbcontext nesnesi ,veri tabanına açılan kapı 
    }


public async Task<List<User>> GetAllAsync()
    {
       return await _context.Users.ToListAsync();
    }
    public async Task<User?>GetByIdAsync(int id)
    {
       return await _context.Users.FindAsync(id);
    }
    public async Task<User?> GetByUsernameAsync(string username)
{
    return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username );
}//koşulu sağlayan ilk kullanıcıyı getir
public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);//bir kullanıcı eklenecek haberi 
        await _context.SaveChangesAsync();//db ye yazan satır 
    }
    public async Task UpdateAsync (User user)
    {
        _context.Users.Update(user); //db ye gitmiyor EF Core un hafızasında değiştirir 
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    } }