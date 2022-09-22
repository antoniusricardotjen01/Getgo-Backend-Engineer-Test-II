using CarSharingBooking.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.Interface
{
    public interface ICarUserBookingRepo
    {
        IEnumerable<CarAndUser> GetAll();
        IEnumerable<CarAndUser> GetDataByUserNameCarID(int userid, int carid);
        void Insert(CarAndUser Input);
        void Update(CarAndUser Input);
    }
}
