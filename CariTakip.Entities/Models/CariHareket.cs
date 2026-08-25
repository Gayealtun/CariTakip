
using CariTakip.Entities.Enums;
namespace CariTakip.Entities;
public class CariHareket : BaseEntity
{
  
    public Guid CariId{get;set;}
    public Cari Cari{get;set;} = null!;
    public DateTime Tarih{get;set;} = DateTime.UtcNow;
    public HareketTuru Tip{get;set;} //borç,alacak
    public string? Aciklama{get;set;}
    public decimal Tutar{get;set;}
    public KaynakTuru Kaynak{get;set;}//fatura tahsilat ödeme 
    public Guid? KaynakId{get;set;}//lgili fatura/tahsilat ıd si 
   

}