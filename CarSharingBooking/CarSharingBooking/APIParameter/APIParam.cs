using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingBooking.APIParameter
{
    public class SearchCar
    {
        [Required]
        public int CoordinateX { get; set; }
        [Required]
        public int CoordinateY { get; set; }
    }

    public class BookCar
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int CoordinateX { get; set; }
        [Required]
        public int CoordinateY { get; set; }
    }

    public class ReachCar
    {
        [Required]
        public int CarID { get; set; }
    }
}
