namespace AseregeBarcelonaWeb.Model.Data
{
    public class AseregeConfiguration
    {
        public string hostname;
        public int port;
        public string username;
        public string password;
        public string databaseName;
        public AseregeConfiguration()
        {
        }
        public AseregeConfiguration(string hotname, int port, string username, string password, string databaseName)
        {
            hostname = hotname;
            this.port = port;
            this.username = username;
            this.password = password;
            this.databaseName = databaseName;
        }
    }
}
