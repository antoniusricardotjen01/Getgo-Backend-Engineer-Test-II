using CarSharingBooking.APIParameter;
using CarSharingBooking.DTOModels;
using CarSharingBooking.Repositories.BusinessService;
using CarSharingBooking.Repositories.DBContext;
using CarSharingBooking.Repositories.Interface;
using CarSharingBooking.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
        [AllowAnonymous]
        public ActionResult<List<CarDTO>> GetCarDetails()
        {
            List<CarDTO> result = new List<CarDTO>();
            _carBusinessFacade.GetAllCarDetails().ToList().ForEach(t => result.Add(CommonUtility.ConvertToCarDTO(t)));
            if (result.Count <= 0)
            {
                return NotFound();
            }
            return result;
        }

        [HttpGet]
        [Route("Search")]
        public ActionResult<List<CarDTO>> SearchCar([FromQuery] SearchCar param)
        {
            List<CarDTO> result = new List<CarDTO>();
            _carBusinessFacade.SearchCar(param.CoordinateX, param.CoordinateY).ToList().
                ForEach(t=> result.Add(CommonUtility.ConvertToCarDTO(t)));
            if (result.Count <= 0)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        [Route("Book")]
        public ActionResult<bool> BookCar([FromForm] BookCar param)
        {
            if (!_carBusinessFacade.BookCar(param.CoordinateX, param.CoordinateY, CommonUtility.SanitizeStringParameter(param.Username)))
            {
                return BadRequest();
            }
            return true;
        }

        [HttpPost]
        [Route("Finish")]
        public ActionResult<bool> ReachCar([FromForm] ReachCar param)
        {
            if (!_carBusinessFacade.ReachCar(param.CarID))
            {
                return BadRequest();
            }
            return true;
        }
    }
}
