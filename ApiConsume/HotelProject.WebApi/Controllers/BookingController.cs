using HotelProject.BusinessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet] //verileri getirir
        public IActionResult ServiceList()
        {
            var values = _bookingService.TGetList();
            return Ok(values);
        }
        [HttpPost] //yeni veri ekleme
        public IActionResult AddService(Booking booking)
        {
            _bookingService.TInsert(booking);
            return Ok();
        }
        [HttpDelete("{id}")] //verileri silme 
        public IActionResult DeleteService(int id)
        {
            var values = _bookingService.TGetByID(id);
            _bookingService.TDelete(values);
            return Ok();
        }
        [HttpPut] //verileri günceller
        public IActionResult UpdateService(Booking booking)
        {
            _bookingService.TUpdate(booking);
            return Ok();
        }
        [HttpGet("{id}")] //dışarıdan bir id parametresi alır
        public IActionResult GetService(int id)
        {
            var values = _bookingService.TGetByID(id);
            return Ok(values);
        }
    }
}
