using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public Nullable<int> VehicleId { get; set; }
        [Required]
        public Nullable<int> ServiceId { get; set; }
        [Required]
        public Nullable<System.DateTime> StartBookingDate { get; set; }
        [Required]
        public Nullable<System.DateTime> EndBookingDate { get; set; }
        public Nullable<int> MechanicId { get; set; }
        public Nullable<int> Status { get; set; }

        public virtual MechanicViewModel Mechanic { get; set; }
        public virtual ServiceViewModel Service { get; set; }
        public virtual VehicleViewModel Vehicle { get; set; }
        public virtual StautsOfBookingViewModel StautsOfBooking { get; set; }
        public IEnumerable<BookingViewModel> bookinglist { get; set; }
    }
}
