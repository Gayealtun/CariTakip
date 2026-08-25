using CariTakip.Entities.Enums;

namespace CariTakip.Business.Dtos;

public class CreateCariHareketDto
{
    public Guid CariId { get; set; }
    public DateTime? Tarih { get; set; }
    public HareketTuru Tip { get; set; }
    public string? Aciklama { get; set; }
    public decimal Tutar { get; set; }
    public KaynakTuru Kaynak { get; set; }
    public Guid? KaynakId { get; set; }//nullable çünkü manuel işlem kayda bağlı değil
}