using HotelProject.WebUI.Models.Testimonial;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
	public class TestimonialController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public TestimonialController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		//await kullanılıyorsa metot async olmalı 
		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			//bir tane istemci oluşturduk
			var responseMessage = await client.GetAsync("http://localhost:2077/api/Testimonial");
			//listemelek istediğimizden getasync kullandık
			if (responseMessage.IsSuccessStatusCode)
			//başarılı durum kodu dönerse
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<TestimonialViewModel>>(jsonData);
				return View(values);
			}

			return View();
		}
		[HttpGet]
		public IActionResult AddTestimonial()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddTestimonial(TestimonialViewModel model)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(model);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			//Encoding.UTF8 türkçe karakter desteklesin
			var responseMessage = await client.PostAsync("http://localhost:2077/api/Testimonial", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> DeleteTestimonial(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"http://localhost:2077/api/Testimonial/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				//Başarılı olursa index'e dönsün 
				return RedirectToAction("Index");
			}
			return View();
			//olmazsa viewvi döndürsün
		}

		[HttpGet]
		public async Task<IActionResult> UpdateTestimonial(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"http://localhost:2077/api/Testimonial/{id}");
			//Güncelleyeceğimiz verileri GetAsync ile önce getiriyoruz
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				//veri listeleme yapıyoruz
				var values = JsonConvert.DeserializeObject<TestimonialViewModel>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateTestimonial(TestimonialViewModel testimonialViewModel)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(testimonialViewModel);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("http://localhost:2077/api/Testimonial/", stringContent);
			//PutAsync bu şekilde tanımlanır $ ve idye gerek yok
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
