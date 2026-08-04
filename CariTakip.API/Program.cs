using Microsoft.EntityFrameworkCore;
using CariTakip.Business;
using System.Text;
using CariTakip.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

//uygulamanın hazırlık aşaması
var builder = WebApplication.CreateBuilder(args); 

//openapi dokümanı oluşturur
builder.Services.AddOpenApi();

//controller sınıflarını sisteme kaydeder
builder.Services.AddControllers();

//business, dataaccess, repository, dbcontext bağımlılıklarını DI a kaydeder
builder.Services.AddBusiness(builder.Configuration.GetConnectionString("DefaultConnection")!);

//JWT token üreten servici DI a kaydeder
builder.Services.AddScoped<JwtTokenService>();

//gelen JWT tokenlarının nasıl doğrulanacağını belirler
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        string key = builder.Configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "JWT anahtarı bulunamadı."
            );

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],

                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key)
                    ),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
    });

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

app.UseCors("ReactFrontend");
//önce tokenı doğrular ve kullanıcı kimliğini belirler
app.UseAuthentication();
//sonra kullanıcının endpointe erişim yetkisini kontrol eder
app.UseAuthorization();
//Controller içindeki endpointleri URL lere bağlar  
app.MapControllers();
app.Run();

