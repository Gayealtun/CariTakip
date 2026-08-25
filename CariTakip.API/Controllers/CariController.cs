using CariTakip.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CariTakip.Entities;
using CariTakip.Business.Dtos;
using CariTakip.Entities.Enums;
using Microsoft.AspNetCore.Authorization;


namespace CariTakip.API.Controllers;

[Authorize]//geçerli token ı olmayan endpointleri kullanamaz
[ApiController] //sınıf bir web api 
[Route("api/[controller]")]
public class CariController : ControllerBase
{
    private readonly ICariService _cariService;

    public CariController(ICariService cariService)
    {
        _cariService = cariService;
    }


    [HttpGet]
    public async Task<ActionResult<List<Cari>>> GetAll()
    {
        List<Cari> cariler = await _cariService.GetAllAsync();
        return Ok(cariler);
    }
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Cari>> GetById([FromRoute] Guid id)
    {
        Cari? cari = await _cariService.GetByIdAsync(id);

        if (cari == null)
        {
            return NotFound("cari bulunamadi");
        }
        return Ok(cari);
    }
    [HttpPost]
    public async Task<ActionResult<Cari>> CreateAsync(CreateCariDto dto)
    {
        try
        {
            Cari cari = new Cari
            {
                Unvan = dto.Unvan,
                VergiNoTC = dto.VergiNoTC,
                Adres = dto.Adres,
                Telefon = dto.Telefon,
                Email = dto.Email,
                Tip = (CariTipi)dto.Tip,
                Iban = dto.Iban,
                AktifMi = dto.AktifMi,
                KrediLimiti = dto.KrediLimiti,
            };
            Cari createdCari = await _cariService.CreateAsync(cari);

            return CreatedAtAction(
             nameof(GetById),//hangi controller metoduyla geri bulunabilir
             new { id = createdCari.Id }, createdCari);//route için gereken değeri verir
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }

    }
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<Cari>> Update([FromRoute] Guid id,
                                                 [FromBody] UpdateCariDto dto)
    {
        try
        {
            Cari? existingCari = await _cariService.GetByIdAsync(id);

            if (existingCari == null)
            {
                return NotFound("Cari bulunamadı");
            }
            existingCari.Unvan = dto.Unvan;
            existingCari.VergiNoTC = dto.VergiNoTC;
            existingCari.Adres = dto.Adres;
            existingCari.Telefon = dto.Telefon;
            existingCari.Email = dto.Email;
            existingCari.Tip = (CariTipi)dto.Tip;
            existingCari.Iban = dto.Iban;
            existingCari.AktifMi = dto.AktifMi;
            existingCari.KrediLimiti = dto.KrediLimiti;

            Cari updatedCari = await _cariService.UpdateAsync(existingCari);
            return Ok(updatedCari);

        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _cariService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
