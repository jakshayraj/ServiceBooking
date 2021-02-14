using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interface
{
    public interface IBookingRepository
    {
        BookingViewModel GetBooking(int? Id);
        IEnumerable<BookingViewModel> GetAllBooking();
        IEnumerable<BookingViewModel> GetAllBookingByCustomer(int id);
        string CreateBooking(BookingViewModel model);
        string UpdateBooking(BookingViewModel model);
        string DeleteBooking(int? Id);
    }
}
