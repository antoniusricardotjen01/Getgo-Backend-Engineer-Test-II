using CarSharingBooking.Repositories.DBContext;
using CarSharingBooking.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.Interface
{
    public class Repository_Car : ICarBookingRepo
    {
        private masterContext _masterContext;

        public Repository_Car()
        {
            _masterContext = new masterContext();
        }  

        public Car GetDataByID(int ID)
        {
            return _masterContext.Cars.Find(ID);
        }

        public void Insert(Car Input)
        {
            _masterContext.Cars.Add(Input);
            _masterContext.SaveChanges();
        }

        public void Update(Car Input)
        {
            _masterContext.Entry(Input).State = EntityState.Modified;
            _masterContext.SaveChanges();
        }

        public IEnumerable<Car> GetAll()
        {
            return _masterContext.Cars.ToList();
        }
    }
}
