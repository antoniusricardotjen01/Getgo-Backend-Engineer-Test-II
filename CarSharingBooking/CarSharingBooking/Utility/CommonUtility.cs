using CarSharingBooking.DTOModels;
using CarSharingBooking.Repositories.DBContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharingBooking.Utility
{
    public static class CommonUtility
    {
        public static CarDTO ConvertToCarDTO(Car Car)
        {
            return new CarDTO() { 
                Status = Car.CarStatus.Value == Constant.Vacant ? Constant.VacantStatus : Constant.OccupiedStatus,
                CoordinateX = Car.CarCoordinateX.Value,
                CoordinateY = Car.CarCoordinateY.Value,
                CarNo = Car.CarId
            };
        }

        public static string SanitizeStringParameter(string param)
        {
            StringBuilder result = new StringBuilder(JsonConvert.SerializeObject(param));
            result.Replace("-", "");
            result.Replace(",", "");
            result.Replace("!", "");
            result.Replace("#", "");
            result.Replace("$", "");
            result.Replace("%", "");
            result.Replace("^", "");
            result.Replace("&", "");
            result.Replace("*", "");
            result.Replace("(", "");
            result.Replace(")", "");
            result.Replace(@"""","");
            return result.ToString();
        }
    }
}
