using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICarBookingRepo CarBooking { get; }
        ICarUserBookingRepo CarUserBooking { get; }

        IUserBookingRepo UserBooking { get; }

        bool Commit();
    }
}
