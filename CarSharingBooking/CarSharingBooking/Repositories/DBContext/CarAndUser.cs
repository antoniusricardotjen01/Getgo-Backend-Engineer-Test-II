using System;
using System.Collections.Generic;

#nullable disable

namespace CarSharingBooking.Repositories.DBContext
{
    public partial class CarAndUser
    {
        public int RecordId { get; set; }
        public int? CarId { get; set; }
        public int? UserId { get; set; }

        public virtual Car Car { get; set; }
        public virtual CarUser User { get; set; }
    }
}
