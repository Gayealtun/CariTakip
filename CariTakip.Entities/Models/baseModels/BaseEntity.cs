
namespace CariTakip.Entities;
public class BaseEntity
{
    public Guid Id {get;set;} = Guid.NewGuid();
   
    public DateTime OlusturmaTarihi {get;set;} =DateTime.UtcNow;
   
    public bool AktifMi{get;set;} = true ;
  

}
