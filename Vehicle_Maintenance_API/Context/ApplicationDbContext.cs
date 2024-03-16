using Microsoft.EntityFrameworkCore;

namespace Vehicle_Maintenance_API.Context
{
    public class ApplicationDbContext: DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
