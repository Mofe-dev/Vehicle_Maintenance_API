using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Vehicle_Maintenance_API.Dto;

namespace Vehicle_Maintenance_API.Repositories
{
    public partial class MasterEntityRepository
    {
        public virtual List<dynamic> SaveBooking(BookingDto booking)
        {
            List<dynamic> ListResult = new List<dynamic>();

            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.InfoMessage += new SqlInfoMessageEventHandler(SqlMessageHandler);

                using (var command = new SqlCommand())
                {
                    command.CommandText = "[dbo].[sp_bookingPost]";
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PlateNumber", booking.PlateNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BookingDate", booking.BookingDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Hour", booking.Hour ?? (object)DBNull.Value);

                    ListResult = DynamicList(command);


                }
                return ListResult;
            }
        }

        public virtual List<dynamic> GetBookings(string plateNumber)
        {
            List<dynamic> ListResult = new List<dynamic>();

            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.InfoMessage += new SqlInfoMessageEventHandler(SqlMessageHandler);

                using (var command = new SqlCommand())
                {
                    command.CommandText = "[dbo].[sp_bookingGet]";
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PlateNumber", plateNumber ?? (object)DBNull.Value);

                    ListResult = DynamicList(command);


                }
                return ListResult;
            }
        }
    }
}
