using CarSharingBooking.Repositories.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.Interface
{
    public class Repository_CarUser : ICarUserBookingRepo
    {
        private masterContext _masterContext;

        public Repository_CarUser()
        {
            _masterContext = new masterContext();
        }

        public IEnumerable<CarAndUser> GetDataByUserNameCarID(int userid, int carid)
        {
            return _masterContext.CarAndUsers.Where(t=>t.CarId.Value == carid && t.UserId.Value == userid);
        }

        public void Insert(CarAndUser Input)
        {
            _masterContext.CarAndUsers.Add(Input);
            _masterContext.SaveChanges();
        }

        public void Update(CarAndUser Input)
        {
            _masterContext.Entry(Input).State = EntityState.Modified;
            _masterContext.SaveChanges();
        }

        public IEnumerable<CarAndUser> GetAll()
        {
            return _masterContext.CarAndUsers.ToList();
        }
    }
}
