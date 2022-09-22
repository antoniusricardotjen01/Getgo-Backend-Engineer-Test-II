using CarSharingBooking.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.Interface
{
    public interface IUserBookingRepo
    {
        IEnumerable<CarUser> GetAll();
        CarUser GetDataByUsername(string username);
        void Insert(CarUser Input);
        void Update(CarUser Input);
    }
}
