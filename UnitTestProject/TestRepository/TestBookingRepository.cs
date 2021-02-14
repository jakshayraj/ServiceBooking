using BusinessEntities;
using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerTests.TestRepository
{
    public class TestBookingRepository : IBookingRepository
    {
        private List<BookingViewModel> _dbContext = new List<BookingViewModel>
        {
            new BookingViewModel{
                BookingId=1,
                VehicleId=1,
                MechanicId=1,
                StartBookingDate=DateTime.Now,
                EndBookingDate=DateTime.Now,
                CustomerId=1,
                Status=1,
                ServiceId=1,
                }
        };

        public string CreateBooking(BookingViewModel model)
        {
            if (model != null)
            {
                int booking = _dbContext.Where(a => a.VehicleId == model.VehicleId).Where(a => a.ServiceId == model.ServiceId).Count();
                if (booking > 0)
                {
                    return "Exist";
                }
                _dbContext.Add(model);
                return "Booking added";
            }

            return "Model is null";
        }

        public string DeleteBooking(int? Id)
        {
            int id = (int)Id;
            BookingViewModel bookingEntitie = _dbContext.Where(s => s.BookingId == id).First();
            _dbContext.Remove(bookingEntitie);
            return "Deleted";
        }

        public IEnumerable<BookingViewModel> GetAllBooking()
        {
            IEnumerable<BookingViewModel> bookingEntities = _dbContext;
            return bookingEntities;
        }

        public IEnumerable<BookingViewModel> GetAllBookingByCustomer(int id)
        {
            IEnumerable<BookingViewModel> Booking = _dbContext.Where(a => a.CustomerId == id);
            return Booking;
        }

        public BookingViewModel GetBooking(int? Id)
        {
            int id = (int)Id;
            BookingViewModel bookingsEntities = _dbContext.Where(s => s.BookingId == id).First();
            return bookingsEntities;
        }

        public string UpdateBooking(BookingViewModel model)
        {
            if (model != null)
            { 
                _dbContext.Add(model);
                return "Booking updated";
            }
            return "Model is null";
        }
    }
}
