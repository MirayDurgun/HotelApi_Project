using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiConsume.Models;


namespace RapidApiConsume.Controllers
{
    public class ImdbController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<ApiMovieViewModel> apiMovieViewModels = new List<ApiMovieViewModel>();

            // HttpClient nesnesi oluşturuluyor.
            var client = new HttpClient();
            // HTTP isteği için HttpRequestMessage oluşturuluyor.
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get, // GET metodu kullanılıyor.
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"), // API'nin URL'si belirleniyor.
                Headers =
        {
            { "X-RapidAPI-Key", "0d63638b74mshf218c5b70b03499p1ae6bbjsn7318835503c5" }, // API isteği için kullanılacak olan anahtar (key).
            { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" }, // API'nin barındırıldığı ana bilgisayar.
        },
            };
            // HttpClient ile istek gönderiliyor ve cevap bekleniyor.
            using (var response = await client.SendAsync(request))
            {
                // Cevap başarılı bir şekilde geldiyse devam ediliyor, aksi halde istisna fırlatılıyor.
                response.EnsureSuccessStatusCode();
                // Cevap içeriği okunuyor ve bir değişkene atanıyor.
                var body = await response.Content.ReadAsStringAsync();

                apiMovieViewModels = JsonConvert.DeserializeObject<List<ApiMovieViewModel>>(body);
                return View(apiMovieViewModels);
            }
        }
    }
}
