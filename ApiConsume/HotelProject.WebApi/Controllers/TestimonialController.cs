using HotelProject.BusinessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestimonialController : ControllerBase
	{
		private readonly ITestimonialService _testimonialService;

		public TestimonialController(ITestimonialService TestimonialService)
		{
			_testimonialService = TestimonialService;
		}

		[HttpGet] //verileri getirir
		public IActionResult TestimonialList()
		{
			var values = _testimonialService.TGetList();
			return Ok(values);
		}
		[HttpPost] //yeni veri ekleme
		public IActionResult AddTestimonial(Testimonial testimonial)
		{
			_testimonialService.TInsert(testimonial);
			return Ok();
		}
		[HttpDelete] //verileri silme 
		public IActionResult DeleteTestimonial(int id)
		{
			var values = _testimonialService.TGetByID(id);
			_testimonialService.TDelete(values);
			return Ok();
		}
		[HttpPut] //verileri günceller
		public IActionResult UpdateTestimonial(Testimonial testimonial)
		{
			_testimonialService.TUpdate(testimonial);
			return Ok();
		}
		[HttpGet("{id}")] //dışarıdan bir id parametresi alır
		public IActionResult GetTestimonial(int id)
		{
			var values = _testimonialService.TGetByID(id);
			return Ok(values);
		}
	}
}
