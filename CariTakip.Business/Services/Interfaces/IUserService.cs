using CariTakip.Entities.Models;

namespace CariTakip.Business.Services.Interfaces;

public interface IUserService
{
    Task <List<User>> GetAllAsync();
    Task <User?>GetByIdAsync(int id);
    Task <User >CreateAsync(User user);
    Task <User?>LoginAsync(string username ,string password);
}