using CarSharingBooking.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.BusinessService
{
    public interface ICarBusinessFacade
    {
        IEnumerable<Car> GetAllCarDetails();

        IEnumerable<Car> SearchCar(int userCoordinateX, int userCoordinateY);

        bool BookCar(int userCoordinateX, int userCoordinateY, string username);

        bool ReachCar(int CarID);
    }
}
