using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApiJwt.Models
{
    public class CreateToken
    {
        public string TokenCreate()
        {
            // Anahtar olarak kullanılacak string değerini UTF-8 formatına dönüştürür
            var bytes = Encoding.UTF8.GetBytes("aspnetcoreapiapi");

            // JWT'nin imzalanması için kullanılacak simetrik anahtarı oluşturur
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);

            // İmzalama yetkilendirmelerini sağlayan bir nesne oluşturur
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // JWT oluşturur
            JwtSecurityToken token = new JwtSecurityToken(
                // JWT'nin düzenleyicisini belirtir
                issuer: "http://localhost",
                // JWT'nin hedef kitlesini belirtir
                audience: "http://localhost",
                // Token'in ne zaman kullanılabileceğini belirtir
                notBefore: DateTime.Now,
                // Token'in ne zaman geçersiz hale geleceğini belirtir
                expires: DateTime.Now.AddSeconds(20),
                // JWT'nin imzalanması için kullanılacak kimlik bilgilerini sağlar
                signingCredentials: credentials
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);

        }
    }
}
