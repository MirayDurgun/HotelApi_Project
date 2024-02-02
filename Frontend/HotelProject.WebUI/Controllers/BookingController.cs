using HotelProject.WebUI.Dtos.BookingDto;
using HotelProject.WebUI.Dtos.SubscribeDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult AddBooking()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(CreateBookingDto createBookingDto)
        {
            createBookingDto.Status = "Onay Bekliyor";

            var client = _httpClientFactory.CreateClient();
            //bir tane istemci oluşturduk
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            //JSON formatına dönüştür
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //Encoding.UTF8 türkçe karakter desteklesin
            var responseMessage = await client.PostAsync("http://localhost:2077/api/Booking", stringContent);
            //JSON verisi, HTTP isteği için uygun formatta olacak şekilde hazırla
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
