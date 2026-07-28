using TaskTracking.Business.Services.Interfaces;
using TaskTracking.DataAccess.Repositories.Interfaces;
using TaskTracking.Entities.Models;

namespace TaskTracking.Business.Services;

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

    public async Task<User?> GetByIdAsync(int id)
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
}