using CarSharingBooking.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.Interface
{
    public class UnitOfWorks : IUnitOfWork
    {
        private readonly masterContext _context;

        public UnitOfWorks(masterContext context, ICarUserBookingRepo caruserbookingrepo,
            ICarBookingRepo carbookingrepo, IUserBookingRepo userbookingrepo)
        {
            _context = context;
            CarBooking = carbookingrepo;
            CarUserBooking = caruserbookingrepo;
            UserBooking = userbookingrepo;
        }

        public ICarBookingRepo CarBooking { get; }

        public ICarUserBookingRepo CarUserBooking { get; }

        public IUserBookingRepo UserBooking { get; }

        public bool Commit() {
            bool result = false;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    dbContextTransaction.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
