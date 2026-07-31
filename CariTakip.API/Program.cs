using Microsoft.EntityFrameworkCore;
using CariTakip.DataAccess.Context;
using CariTakip.Business;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using CariTakip.Business.Services.Interfaces;
using CariTakip.DataAccess.Repositories;
using CariTakip.DataAccess.Repositories.Interfaces;
using CariTakip.Business.Services;
using CariTakip.DataAccess;

//uygulamanın hazırlık aşaması
var builder = WebApplication.CreateBuilder(args); 

//openapi dokümanı oluşturur
builder.Services.AddOpenApi();

//controller sınıflarını sisteme kaydeder
builder.Services.AddControllers();

//business data access repository dbcontext kayıtlarını yapar,DI metodu yani 
builder.Services.AddBusiness(builder.Configuration.GetConnectionString("DefaultConnection")!);


var app = builder.Build ();
if (app.Environment.IsDevelopment()){
    app.MapOpenApi(); //kullanıcının görmesi istenmez bu yüzden development içinde 
    app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/openapi/v1.json",
        "CariTakip API"
    );
});
}

app.UseHttpsRedirection();
//Controller içindeki endpointleri URL lere bağlar  
app.MapControllers();
app.Run();

//eğer biri db kullanmak isterse , applicationdbcontext oluştur ve onu CariTakip.db 
//dosyasına bağlanancak şekilde hazırla 
