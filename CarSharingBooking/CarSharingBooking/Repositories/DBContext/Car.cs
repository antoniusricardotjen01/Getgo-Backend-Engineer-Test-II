using System;
using System.Collections.Generic;

#nullable disable

namespace CarSharingBooking.Repositories.DBContext
{
    public partial class Car
    {
        public Car()
        {
            CarAndUsers = new HashSet<CarAndUser>();
        }

        public int CarId { get; set; }
        public int? CarCoordinateX { get; set; }
        public int? CarCoordinateY { get; set; }
        public int? CarStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<CarAndUser> CarAndUsers { get; set; }
    }
}
