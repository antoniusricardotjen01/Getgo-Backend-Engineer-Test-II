using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.DTOModels
{
    public class CarDTO
    {
        public int CarNo { get; set; }

        public int CoordinateY { get; set; }

        public int CoordinateX { get; set; }

        public string Status { get; set; }
    }
}
