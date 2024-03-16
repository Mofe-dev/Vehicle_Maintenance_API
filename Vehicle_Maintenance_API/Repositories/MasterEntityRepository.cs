using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Text;
using Vehicle_Maintenance_API.Context;
using Vehicle_Maintenance_API.Dto;
using Vehicle_Maintenance_API.Interfaces;

namespace Vehicle_Maintenance_API.Repositories
{
    public partial class MasterEntityRepository : IMasterEntityRepository
    {
        private ApplicationDbContext _context;
        private static SqlMessages _messages = new SqlMessages { Level = SqlLevelMessages.INFORMATION };


        public MasterEntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public string GetConnectionString()
        {
            try
            {
                return _context.Database.GetDbConnection().ConnectionString;

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in get conection string [MasterEntityRepository]: " + ex.Message);
            }

        }

        public static void SqlMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            try
            {
                _messages.Level = SqlLevelMessages.INFORMATION;
                _messages.Messages = _messages.Messages ?? new List<string>();

                _messages.Messages.Add(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SqlMessageHandler: " + ex.Message);
            }
        }

        public List<dynamic> DynamicList(DbCommand command)
        {
            List<dynamic> ListResult = new List<dynamic>();

            StringBuilder Builder = new StringBuilder();

            _messages = new SqlMessages { Level = SqlLevelMessages.INFORMATION };

            try
            {
                command.Connection.Open();

                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                // Retrieve the Data.
                // DbDataReader reader = command.ExecuteReader();

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string line = reader.GetString(0);

                        Builder.Append(line);
                    }

                    if (reader.NextResult())
                        reader.Read();

                    // Get the values
                    string StringJson = Builder.ToString();

                    ListResult = JsonConvert.DeserializeObject<List<dynamic>>(StringJson);
                }
            }
            catch (SqlException sqlException)
            {
                if (sqlException.Number == 50001)
                {
                    _messages.Level = SqlLevelMessages.ERROR;
                    _messages.Messages = new List<string> { sqlException.Message };
                }

                Debug.WriteLine("SQL Error : {0}", sqlException.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SQL Connection Error : {0}", ex.Message);
            }
            finally
            {
                if (command.Connection.State != ConnectionState.Closed)
                {
                    command.Connection.Close();
                }
            }

            return ListResult ?? new List<dynamic>();
        }
    }
}
