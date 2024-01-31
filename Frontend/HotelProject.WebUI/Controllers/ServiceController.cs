using HotelProject.WebUI.Dtos.ServiceDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //await kullanılıyorsa metot async olmalı 
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            //bir tane istemci oluşturduk
            var responseMessage = await client.GetAsync("http://localhost:2077/api/Service");
            //listemelek istediğimizden getasync kullandık
            if (responseMessage.IsSuccessStatusCode)
            //başarılı durum kodu dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddService(CreateServiceDto createServiceDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createServiceDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //Encoding.UTF8 türkçe karakter desteklesin
            var responseMessage = await client.PostAsync("http://localhost:2077/api/Service", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> DeleteService(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:2077/api/Service/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                //Başarılı olursa index'e dönsün 
                return RedirectToAction("Index");
            }
            return View();
            //olmazsa viewvi döndürsün
        }
    }
}
