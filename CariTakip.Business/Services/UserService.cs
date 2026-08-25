using CariTakip.Business.Services.Interfaces;
using CariTakip.DataAccess.Repositories.Interfaces;
using CariTakip.Entities.Models;
using CariTakip.Business.Dtos;

namespace CariTakip.Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateAsync(User user)
    {
        User? existingUser =
        await _userRepository.GetByUsernameAsync(user.UserName);

        if(existingUser != null)
        {
            throw new InvalidOperationException("Bu kullanıcı adı zaten mevcut");
        }
        await _userRepository.AddAsync(user);

        return user;
    }

    public async Task<User?> LoginAsync(
        string username,
        string password)
    {
        User? user =
            await _userRepository.GetByUsernameAsync(username);

        if (user == null)
        {
            return null;
        }

        if (user.Password != password)
        {
            return null;
        }

        return user;
    }
    public async Task<User?> GetProfileAsync(Guid userId)
{
    return await _userRepository.GetByIdAsync(userId);
}

public async Task<User?> UpdateProfileAsync(
    Guid userId,
    UpdateProfileDto dto)
{
    var user = await _userRepository.GetByIdAsync(userId);

    if (user == null)
    {
        return null;
    }

    user.FirstName = dto.FirstName;
    user.LastName = dto.LastName;
    user.Gender = dto.Gender;
    user.BirthDate = dto.BirthDate;
    user.NationalId = dto.NationalId;
    user.UserName = dto.UserName;

    await _userRepository.UpdateAsync(user);

    return user;
}
}