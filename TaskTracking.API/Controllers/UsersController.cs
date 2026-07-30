using Microsoft.AspNetCore.Mvc;
using TaskTracking.Business.Services.Interfaces;
using TaskTracking.Entities.Models;

namespace TaskTracking.API.Controllers;
[ApiController]
[Route ("api/[controller]")]

public class UsersController : ControllerBase
{
    private readonly IUserService _UserService;

    public UsersController(IUserService userService)
    {
        _UserService = userService;
    }
    //tüm kullanıcıları getir 
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        List <User> users= await _UserService.GetAllAsync();
        return Ok(users);
    }
    //id ye göre kullanıcı getir 
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetById([FromRoute]int id)
    {
        User? user = await _UserService.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    //yeni kullanıcı oluştur 
    [HttpPost]
    public async Task<ActionResult<User>>Create(User user)
    {
        //service çağırılıyor 
        User createdUser = await _UserService.CreateAsync(user);
        return Ok(createdUser);
    }
    //giriş yap
    [HttpPost("login")]
    public async Task <ActionResult<User>> Login(User loginUser)
    {
        User? user = await _UserService.LoginAsync(
            loginUser.UserName,
            loginUser.Password );

            if (user == null)
        {
            return Unauthorized();
        }
        return Ok(user);
    }
}

