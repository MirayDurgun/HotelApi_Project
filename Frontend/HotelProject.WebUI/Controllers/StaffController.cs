using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotelProject.WebUI.Models;
using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace HotelProject.WebUI.Controllers
{
    //personel controller
    public class StaffController : Controller
    //api consume
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StaffController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //await kullanılıyorsa metot async olmalı 
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            //bir tane istemci oluşturduk
            var responseMessage = await client.GetAsync("http://localhost:2077/api/Staff");
            //listemelek istediğimizden getasync kullandık
            if (responseMessage.IsSuccessStatusCode)
            //başarılı durum kodu dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<StaffViewModel>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStaff(AddStaffViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //Encoding.UTF8 türkçe karakter desteklesin
            var responseMessage = await client.PostAsync("http://localhost:2077/api/Staff", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:2077/api/Staff/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                //Başarılı olursa index'e dönsün 
                return RedirectToAction("Index");
            }
            return View();
            //olmazsa viewvi döndürsün
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:2077/api/Staff/{id}");
            //Güncelleyeceğimiz verileri GetAsync ile önce getiriyoruz
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                //veri listeleme yapıyoruz
                var values = JsonConvert.DeserializeObject<UpdateStaffViewModel>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStaff(UpdateStaffViewModel updateStaffViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateStaffViewModel);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:2077/api/Staff/", stringContent);
            //PutAsync bu şekilde tanımlanır $ ve idye gerek yok
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
