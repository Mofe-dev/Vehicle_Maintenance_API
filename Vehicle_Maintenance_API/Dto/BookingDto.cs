namespace Vehicle_Maintenance_API.Dto
{
    public class BookingDto
    {
        public Guid? BookingId { get; set; }
        public required string PlateNumber { get; set; }
        public required string BookingDate { get; set; }
        public required Guid? BookingStatus { get; set; }
        public required string Hour { get; set; }


    }
}
