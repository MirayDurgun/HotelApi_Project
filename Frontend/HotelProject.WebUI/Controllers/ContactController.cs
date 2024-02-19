
using HotelProject.BusinessLayer.Abstract;
using HotelProject.WebUI.Dtos.ContactDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateContactDto createContactDto)
        {
            createContactDto.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

            var client = _httpClientFactory.CreateClient();
            //bir tane istemci oluşturduk
            var jsonData = JsonConvert.SerializeObject(createContactDto);
            //JSON formatına dönüştür
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //Encoding.UTF8 türkçe karakter desteklesin
            var responseMessage = await client.PostAsync("http://localhost:2077/api/Contact", stringContent);
            //JSON verisi, HTTP isteği için uygun formatta olacak şekilde hazırla
            return RedirectToAction("Index", "Default");

        }
    }
}
