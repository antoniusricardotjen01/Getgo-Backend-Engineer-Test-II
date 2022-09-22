using CarSharingBooking.APIParameter;
using CarSharingBooking.DTOModels;
using CarSharingBooking.Repositories.BusinessService;
using CarSharingBooking.Repositories.DBContext;
using CarSharingBooking.Repositories.Interface;
using CarSharingBooking.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarSharingBooking.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarBookingController : ControllerBase
    {
        ICarBusinessFacade _carBusinessFacade;
        public CarBookingController(ICarBusinessFacade carbusinessfacade)
        {
            _carBusinessFacade = carbusinessfacade;
        }

        [HttpGet]
        [Route("Car")]
        public IActionResult GetCarDetails()
        {
            List<CarDTO> result = new List<CarDTO>();
            _carBusinessFacade.GetAllCarDetails().ToList().ForEach(t => result.Add(DTOMapper.ConvertToCarDTO(t)));
            if (result.Count <= 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult SearchCar([FromQuery] SearchCar param)
        {
            List<CarDTO> result = new List<CarDTO>();
            _carBusinessFacade.SearchCar(param.CoordinateX, param.CoordinateY).ToList().
                ForEach(t=> result.Add(DTOMapper.ConvertToCarDTO(t)));
            if (result.Count <= 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Book")]
        public IActionResult BookCar([FromBody] BookCar param)
        {
            if (!_carBusinessFacade.BookCar(param.CoordinateX, param.CoordinateY, param.Username))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("Finish")]
        public IActionResult ReachCar([FromBody] ReachCar param)
        {
            if (!_carBusinessFacade.ReachCar(param.CarID))
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
