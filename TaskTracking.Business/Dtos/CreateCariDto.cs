namespace TaskTracking.Business.Dtos;

public class CreateCariDto{
    public string Unvan{get;set;} = string.Empty;
    public string? VergiNoTC{get;set;}
    public string? Adres { get; set; }
    public string? Telefon { get; set; }
    public string? Email { get; set; }
    public int Tip { get; set; }
    public string? Iban { get; set; }
    public bool AktifMi { get; set; } = true;
    public decimal KrediLimiti { get; set; }
}