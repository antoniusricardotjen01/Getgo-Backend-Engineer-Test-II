using CarSharingBooking.Repositories.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.Interface
{
    public class Repository_User : IUserBookingRepo
    {
        private masterContext _masterContext;

        public Repository_User()
        {
            _masterContext = new masterContext();
        }

        public CarUser GetDataByUsername(string username)
        {
            return _masterContext.CarUsers.Where(t => t.UserBookingName == username).FirstOrDefault();
        }

        public void Insert(CarUser Input)
        {
            _masterContext.CarUsers.Add(Input);
            _masterContext.SaveChanges();
        }

        public void Update(CarUser Input)
        {
            _masterContext.Entry(Input).State = EntityState.Modified;
            _masterContext.SaveChanges();
        }

        public IEnumerable<CarUser> GetAll()
        {
            return _masterContext.CarUsers.ToList();
        }
    }
}
