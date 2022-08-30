namespace POC.WebAPI.Repository
{
    public class BaseDbContext
    {
        protected string _connectionString;
        public IConfigurationRoot GetAppSettings()
        {
            string applicationExeDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var builder = new ConfigurationBuilder()
            .SetBasePath(applicationExeDirectory)
            .AddJsonFile("appsettings.json");

            return builder.Build();
        }


        public BaseDbContext()
        {
            var appSettingsJson = GetAppSettings();
            var connectionString = appSettingsJson["ConnectionStrings:DefaultConnection"];
            _connectionString = connectionString;
        }

        protected void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
