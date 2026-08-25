using Microsoft.AspNetCore.Mvc;
using CariTakip.Business.Services.Interfaces;
using CariTakip.Entities.Models;
using CariTakip.API.Services;
using CariTakip.Business.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<User>> GetById([FromRoute]Guid id)
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
    [Authorize]
[HttpGet("me")]
public async Task<IActionResult> GetProfile()
{
    string? userIdValue =
        User.FindFirstValue(ClaimTypes.NameIdentifier);

    if (!Guid.TryParse(userIdValue, out Guid userId))
    {
        return Unauthorized();
    }

    var user = await _UserService.GetProfileAsync(userId);

    if (user == null)
    {
        return NotFound();
    }

    return Ok(new
    {
        user.Id,
        user.FirstName,
        user.LastName,
        user.Gender,
        user.BirthDate,
        user.NationalId,
        user.UserName
    });
}
[Authorize]
[HttpPut("me")]
public async Task<IActionResult> UpdateProfile(
    UpdateProfileDto dto)
{
    string? userIdValue =
        User.FindFirstValue(ClaimTypes.NameIdentifier);

    if (!Guid.TryParse(userIdValue, out Guid userId))
    {
        return Unauthorized();
    }

    var user =
        await _UserService.UpdateProfileAsync(userId, dto);

    if (user == null)
    {
        return NotFound();
    }

    return Ok(new
    {
        user.Id,
        user.FirstName,
        user.LastName,
        user.Gender,
        user.BirthDate,
        user.NationalId,
        user.UserName
    });
}
}

