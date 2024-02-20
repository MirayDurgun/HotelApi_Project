using HotelProject.BusinessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet] //verileri getirir
        public IActionResult GuestList()
        {
            var values = _guestService.TGetList();
            return Ok(values);
        }
        [HttpPost] //yeni veri ekleme
        public IActionResult AddGuest(Guest guest)
        {
            _guestService.TInsert(guest);
            return Ok();
        }
        [HttpDelete("{id}")] //verileri silme 
        public IActionResult DeleteGuest(int id)
        {
            var values = _guestService.TGetByID(id);
            _guestService.TDelete(values);
            return Ok();
        }
        [HttpPut] //verileri günceller
        public IActionResult UpdateGuest(Guest guest)
        {
            _guestService.TUpdate(guest);
            return Ok();
        }
        [HttpGet("{id}")] //dışarıdan bir id parametresi alır
        public IActionResult GetGuest(int id)
        {
            var values = _guestService.TGetByID(id);
            return Ok(values);
        }
    }
}
