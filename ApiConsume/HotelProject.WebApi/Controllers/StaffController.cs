using System.Security.AccessControl;
using HotelProject.BusinessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StaffController : ControllerBase
	{
		private readonly IStaffService _staffService;

		public StaffController(IStaffService staffService)
		{
			_staffService = staffService;
		}

		[HttpGet] //verileri getirir
		public IActionResult StaffList()
		{
			var values = _staffService.TGetList();
			return Ok(values);
		}
		[HttpPost] //yeni veri ekleme
		public IActionResult AddStaff(Staff staff)
		{
			_staffService.TInsert(staff);
			return Ok();
		}
		[HttpDelete] //verileri silme 
		public IActionResult DeleteStaff(int id)
		{
			var values = _staffService.TGetByID(id);
			_staffService.TDelete(values);
			return Ok();
		}
		[HttpPut] //verileri günceller
		public IActionResult UpdateStaff(Staff staff)
		{
			_staffService.TUpdate(staff);
			return Ok();
		}
		[HttpGet("{id}")] //dışarıdan bir id parametresi alır
		public IActionResult GetStaff(int id)
		{
			var values = _staffService.TGetByID(id);
			return Ok(values);
		}
	}
}
