using CarSharingBooking.DTOModels;
using CarSharingBooking.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Utility
{
    public static class DTOMapper
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
    }
}
