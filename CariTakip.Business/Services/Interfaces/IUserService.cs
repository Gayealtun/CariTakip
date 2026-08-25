using CariTakip.Entities.Models;
using CariTakip.Business.Dtos;

namespace CariTakip.Business.Services.Interfaces;

public interface IUserService
{
    Task <List<User>> GetAllAsync();
    Task <User?>GetByIdAsync(Guid id);
    Task <User >CreateAsync(User user);
    Task <User?>LoginAsync(string username ,string password);
    Task<User?> GetProfileAsync(Guid userId);
    

Task<User?> UpdateProfileAsync(
    Guid userId,
    UpdateProfileDto dto
);
}