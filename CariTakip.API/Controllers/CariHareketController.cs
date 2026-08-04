using CariTakip.Business.Services.Interfaces;
using CariTakip.Entities;
using Microsoft.AspNetCore.Mvc;
using CariTakip.Business.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace CariTakip.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]

public class CariHareketController : ControllerBase
{
    private readonly ICariHareketService _cariHareketService;

    public CariHareketController (ICariHareketService cariHareketService)
    {
        _cariHareketService = cariHareketService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var hareketler = await _cariHareketService.GetAllAsync();
        return Ok(hareketler);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var hareket = await _cariHareketService.GetByIdAsync(id);

        if(hareket is null)
        {
            return NotFound();
        }
        return Ok(hareket);
    }

    [HttpGet("cari/{cariId:int}")]
    public async Task<IActionResult> GetByCariId(int cariId)
    {
        var hareketler = await _cariHareketService.GetByCariIdAsync(cariId);

        return Ok(hareketler);
    }
    [HttpGet("cari/{cariId:int}/bakiye")]
public async Task<IActionResult> GetBakiye(int cariId)
{
    decimal bakiye =
        await _cariHareketService.GetBakiyeAsync(cariId);

    return Ok(new
    {
        cariId,
        bakiye
    });
}

    [HttpPost] 
    public async Task <IActionResult> Create(CreateCariHareketDto dto)
    {

       var hareket =
        await _cariHareketService.CreateAsync(dto);

        return Ok(new
    {
        hareket.Id,
        hareket.CariId,
        hareket.Tarih,
        hareket.Tip,
        hareket.Aciklama,
        hareket.Tutar,
        hareket.Kaynak,
        hareket.KaynakId,
        hareket.OlusturmaTarihi
    });
    }

    [HttpPut ("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCariHareketDto dto)
    {
        await _cariHareketService.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult>Delete(int id){

        //controller ın yaptığı iş
        await _cariHareketService.DeleteAsync(id);
        return NoContent();

    }
}
