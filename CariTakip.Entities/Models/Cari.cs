
using CariTakip.Entities.Enums;
namespace CariTakip.Entities;
public class Cari : BaseEntity
{
 
    public string Unvan{get;set;} = string.Empty;
    public string? VergiNoTC{get; set;}
    public string? Adres {get; set;}
    public string? Telefon{get; set;}
    public string? Email {get;set;}
    public CariTipi Tip{get; set;}

    public string? Iban{get;set;}
  
    public decimal KrediLimiti{get;set;}
    public ICollection <CariHareket> Hareketler {get;set;} =new List <CariHareket>();
}
