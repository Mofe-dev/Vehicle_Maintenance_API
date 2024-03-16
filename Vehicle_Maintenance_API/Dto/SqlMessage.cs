namespace Vehicle_Maintenance_API.Dto
{
    public class SqlMessages
    {
        public string Level { get; set; }
        public List<string> Messages { get; set; }
    }

    public class SqlLevelMessages
    {
        public static string INFORMATION { get; } = "INFORMATION";
        public static string WARNING { get; } = "WARNING";
        public static string ERROR { get; } = "ERROR";
    }
}
