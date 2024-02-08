using HotelProject.WebUI.Dtos.BookingDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class BookingAdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingAdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //await kullanılıyorsa metot async olmalı 
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            //bir tane istemci oluşturduk
            var responseMessage = await client.GetAsync("http://localhost:2077/api/Booking");
            //listemelek istediğimizden getasync kullandık
            if (responseMessage.IsSuccessStatusCode)
            //başarılı durum kodu dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
