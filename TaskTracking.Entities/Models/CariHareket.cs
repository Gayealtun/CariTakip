
using TaskTracking.Entities.Enums;
namespace TaskTracking.Entities;
public class CariHareket
{
    public int Id{get;set;}
    public int CariId{get;set;}
    public Cari Cari{get;set;} = null!;
    public DateTime Tarih{get;set;} = DateTime.UtcNow;
    public HareketTuru Tip{get;set;} //borç,alacak
    public string? Açiklama{get;set;}
    public decimal Tutar{get;set;}
    public KaynakTuru Kaynak{get;set;}//fatura tahsilat ödeme 
    public int? KaynakId{get;set;}//lgili fatura/tahsilat ıd si 
    public DateTime OlusturmaTarihi{get;set;}= DateTime.UtcNow;

}