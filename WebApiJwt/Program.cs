using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Kimlik do�rulama i�lemlerini yap�land�r�r
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    // HTTPS kullan�m�n� zorunlu k�lmaz
    opt.RequireHttpsMetadata = false;

    // Token do�rulama parametrelerini ayarlar
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        // Ge�erli Issuer'� belirtir. JWT'yi d�zenleyen veya olu�turan taraf� temsil eder. Yani, tokenin hangi kaynaktan geldi�ini belirtir.kd
        ValidIssuer = "http://localhost",

        // Ge�erli Audience'� belirtir. JWT'nin hedef kitlesini temsil eder. Yani, tokenin hangi kaynak veya servise hitap etti�ini belirtir.
        ValidAudience = "http://localhost",

        // Issuer'� do�rulamak i�in kullan�lacak anahtar� belirtir
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetcoreapiapi")),

        // Issuer'�n imzalama anahtar�n� do�rulamas�n� sa�lar
        ValidateIssuerSigningKey = true,

        // Token'�n s�resini do�rular
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


