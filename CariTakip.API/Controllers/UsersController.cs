using Microsoft.AspNetCore.Mvc;
using CariTakip.Business.Services.Interfaces;
using CariTakip.Entities.Models;
using CariTakip.API.Services;
using CariTakip.Business.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace CariTakip.API.Controllers;
[ApiController]
[Route ("api/[controller]")]

public class UsersController : ControllerBase
{
    private readonly IUserService _UserService;
    private readonly JwtTokenService _jwtTokenService;

    public UsersController(IUserService userService, JwtTokenService jwtTokenService)
    {
        _UserService = userService;
         _jwtTokenService = jwtTokenService;
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
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task <ActionResult<User>> Login(LoginDto dto)
    {
        User? user = await _UserService.LoginAsync(
            dto.UserName,
           dto.Password );

            if (user == null)
        {
            return Unauthorized(new {message =" kullanici adi veya şifre hatali"});
        }
        string token = _jwtTokenService.CreateToken(user);

        return Ok(new
        {
            token ,
            userId= user.Id,
            userName = user.UserName,
            firstName = user.FirstName,
            lastName = user.LastName

        });
    }
}

