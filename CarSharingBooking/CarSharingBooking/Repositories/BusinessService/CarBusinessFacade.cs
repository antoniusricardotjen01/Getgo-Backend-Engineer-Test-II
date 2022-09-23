using CarSharingBooking.Repositories.DBContext;
using CarSharingBooking.Repositories.Interface;
using CarSharingBooking.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.BusinessService
{
    public class CarBusinessFacade : ICarBusinessFacade
    {
        public IUnitOfWork _unitOfWork;
        public CarBusinessFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Car> GetAllCarDetails()
        {
            return _unitOfWork.CarBooking.GetAll();
        }

        public IEnumerable<Car> SearchCar(int userCoordinateX, int userCoordinateY)
        {
            return GetAllCarDetails().
                    Where(car => car.CarStatus == Constant.Vacant &&
                         (car.CarCoordinateX - userCoordinateX) + (car.CarCoordinateY - userCoordinateY) <= 2).
                         OrderBy(x => x.CarCoordinateX).ThenBy(x=>x.CarCoordinateY);
        }

        public bool BookCar(int userCoordinateX, int userCoordinateY, string username)
        {
            //Need to search the car first ?
            bool result = false;
            IEnumerable<Car> AvailableCar = SearchCar(userCoordinateX, userCoordinateY);
            CarUser ExistingUser = _unitOfWork.UserBooking.GetDataByUsername(username);
            if (AvailableCar != null && AvailableCar.Count() >0
                && ExistingUser != null)
            {
                var NearestCar = AvailableCar.First();
                NearestCar.CarStatus = Constant.Occupied;
                NearestCar.ModifiedDate = DateTime.Now;

                CarAndUser mapping = new CarAndUser() 
                { 
                    UserId = ExistingUser.UserId, CarId = NearestCar.CarId
                };

                _unitOfWork.CarBooking.Update(NearestCar);
                _unitOfWork.CarUserBooking.Insert(mapping);
                result = _unitOfWork.Commit();
            }
            return result;
        }

        public bool ReachCar(int CarID)
        {
            bool result = false;
            Car Car = _unitOfWork.CarBooking.GetDataByID(CarID);
            if (Car != null)
            {
                Car.CarStatus = Constant.Vacant;
                Car.ModifiedDate = DateTime.Now;
                _unitOfWork.CarBooking.Update(Car);
                result = _unitOfWork.Commit();
            }
            return result;
        }
    }
}
