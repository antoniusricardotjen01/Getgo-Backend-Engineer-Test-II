using System;
using System.Collections.Generic;

#nullable disable

namespace CarSharingBooking.Repositories.DBContext
{
    public partial class CarUser
    {
        public CarUser()
        {
            CarAndUsers = new HashSet<CarAndUser>();
        }

        public int UserId { get; set; }
        public string UserBookingName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<CarAndUser> CarAndUsers { get; set; }
    }
}
