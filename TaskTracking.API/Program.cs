using Microsoft.EntityFrameworkCore;
using TaskTracking.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=TaskTracking.db");
});

builder.Services.AddOpenApi();

var app = builder.Build ();
if (app.Environment.IsDevelopment()){
    app.MapOpenApi(); //kullanıcının görmesi istenmez bu yüzden development içinde 
}

app.UseHttpsRedirection();
app.Run();

//eğer biri db kullanmak isterse , applicationdbcontext oluştur ve onu tasktracking.db 
//dosyasına bağlanancak şekilde hazırla 