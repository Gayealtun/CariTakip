using Microsoft.EntityFrameworkCore; //Dbcontext kullanılacak 
using TaskTracking.Entities.Models; //userları burada oluşturmuştuk 

namespace TaskTracking.DataAccess.Context;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext( //constructor 
        DbContextOptions<ApplicationDbContext> options ) //db ye nasıl bağlanacak 
        : base (options) //bu ayarlari üst sinifim  olan dbcontexte gönder 
        {} // boş çünkü constructor ın tek işi options ı üst sinifa göndermek

        public DbSet<User> Users {get; set; }//user entitylerinden oluşan bir koleksiyonu
}                                            // veritabanında yönet 
//User class'ını veritabanında tablo olarak kullanacağım.