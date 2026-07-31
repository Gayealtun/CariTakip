using CariTakip.Entities.Models;

namespace CariTakip.DataAccess.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List <User>>GetAllAsync ();
    Task<User?>GetByIdAsync(int id);//? boş olma ihtimali için,list yok çünkü tek değer 
    Task<User?>GetByUsernameAsync(string username);
    Task AddAsync (User user);
    Task UpdateAsync (User user);
    Task DeleteAsync (User user);
    //

}