using Vehicle_Maintenance_API.Dto;

namespace Vehicle_Maintenance_API.Interfaces
{
    public interface IMasterEntityBooking
    {
        List<dynamic> SaveBooking(BookingDto booking);
        List<dynamic> GetBookings(string plateNumber);
    }
}
