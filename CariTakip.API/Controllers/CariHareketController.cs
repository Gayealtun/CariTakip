using CariTakip.Business.Services.Interfaces;
using CariTakip.Entities;
using Microsoft.AspNetCore.Mvc;
using CariTakip.Business.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime.InteropServices;

namespace CariTakip.API.Controllers;

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

    [HttpGet("cariId:id")]
    public async Task<IActionResult> GetByCariId(int cariId)
    {
        var hareketler = await _cariHareketService.GetByCariIdAsync(cariId);

        return Ok(hareketler);
    }

    [HttpPost] 
    public async Task <IActionResult> Create(CreateCariHareketDto dto)
    {
        //dtodaki verileri gerçek entitye aktarma 
        var CariHareket = new CariHareket
        { 
        CariId = dto.CariId,
        Tarih = dto.Tarih ?? DateTime.UtcNow,
        Tip = dto.Tip,
        Aciklama = dto.Aciklama,
        Tutar = dto.Tutar,
        Kaynak = dto.Kaynak,
        KaynakId = dto.KaynakId,
        OlusturmaTarihi = DateTime.UtcNow
        };
       //service çağrısı 
        await _cariHareketService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new{id=CariHareket.Id},
            CariHareket
        );
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCariHareketDto dto,int id)
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
