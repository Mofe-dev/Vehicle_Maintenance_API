namespace Vehicle_Maintenance_API.Interfaces
{
    public interface IMasterEntityRepository: IMasterEntityBooking
    {
        void Dispose();
        string GetConnectionString();
    }
}
