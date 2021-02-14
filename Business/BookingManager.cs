using Business.Interface;
using BusinessEntities;
using Data.Repository.Interface;
using System.Collections.Generic;

namespace Business
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _BookingRepository;

        public BookingManager(IBookingRepository BookingRepository)
        {
            _BookingRepository = BookingRepository;
        }

        public IEnumerable<BookingViewModel> GetAllBooking()
        {
            return _BookingRepository.GetAllBooking();
        }
        public BookingViewModel GetBooking(int? Id)
        {
            return _BookingRepository.GetBooking(Id);
        }
        public string CreateBooking(BookingViewModel model)
        {
            return _BookingRepository.CreateBooking(model);
        }
        public string UpdateBooking(BookingViewModel model)
        {
            return _BookingRepository.UpdateBooking(model);
        }
        public string DeleteBooking(int? Id)
        {
            return _BookingRepository.DeleteBooking(Id);
        }
        public IEnumerable<BookingViewModel> GetAllBookingByCustomer(int id)
        {
            return _BookingRepository.GetAllBookingByCustomer(id);
        }
    }
}
