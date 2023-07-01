using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace HotelProject.WebUI.Controllers
{
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
			var client = _httpClientFactory.CreateClient(); //bir tane istemci oluşturduk
			var responseMessage = await client.GetAsync("http://localhost:2077/api/Staff");
			//listemelek istediğimizden getasync kullandık
			if (responseMessage.IsSuccessStatusCode) //başarılı durum kodu dönerse
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
			var responsiveMessage = await client.PostAsync("http://localhost:2077/api/Staff", stringContent);
			if(responsiveMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
