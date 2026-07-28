using Microsoft.EntityFrameworkCore;
using TaskTracking.DataAccess.Context;
using TaskTracking.Business;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
//uygulamanın hazırlık aşaması
var builder = WebApplication.CreateBuilder(args); 

//openapi dokümanı oluşturur
builder.Services.AddOpenApi();
//controller sınıflarını sisteme kaydeder
builder.Services.AddControllers();
//business data access repository dbcontext kayıtlarını yapar,DI metodu yani 
builder.Services.AddBusiness(builder.Configuration.GetConnectionString("DefaultConnection")!
);

var app = builder.Build ();
if (app.Environment.IsDevelopment()){
    app.MapOpenApi(); //kullanıcının görmesi istenmez bu yüzden development içinde 
    app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/openapi/v1.json",
        "TaskTracking API"
    );
});
}

app.UseHttpsRedirection();
//Controller içindeki endpointleri URL lere bağlar  
app.MapControllers();
app.Run();

//eğer biri db kullanmak isterse , applicationdbcontext oluştur ve onu tasktracking.db 
//dosyasına bağlanancak şekilde hazırla 
