using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Kimlik doðrulama iþlemlerini yapýlandýrýr
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    // HTTPS kullanýmýný zorunlu kýlmaz
    opt.RequireHttpsMetadata = false;

    // Token doðrulama parametrelerini ayarlar
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        // Geçerli Issuer'ý belirtir. JWT'yi düzenleyen veya oluþturan tarafý temsil eder. Yani, tokenin hangi kaynaktan geldiðini belirtir.kd
        ValidIssuer = "http://localhost",

        // Geçerli Audience'ý belirtir. JWT'nin hedef kitlesini temsil eder. Yani, tokenin hangi kaynak veya servise hitap ettiðini belirtir.
        ValidAudience = "http://localhost",

        // Issuer'ý doðrulamak için kullanýlacak anahtarý belirtir
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetcoreapiapi")),

        // Issuer'ýn imzalama anahtarýný doðrulamasýný saðlar
        ValidateIssuerSigningKey = true,

        // Token'ýn süresini doðrular
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


