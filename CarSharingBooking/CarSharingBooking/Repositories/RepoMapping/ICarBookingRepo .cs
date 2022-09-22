using CarSharingBooking.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.Interface
{
    public interface ICarBookingRepo
    {
        IEnumerable<Car> GetAll();
        Car GetDataByID(int ID);
        void Insert(Car Input);
        void Update(Car Input);
    }
}
