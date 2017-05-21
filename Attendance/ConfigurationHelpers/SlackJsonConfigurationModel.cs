namespace Attendance.ConfigurationHelpers
{
    public class SlackJsonConfigurationModel
    {
        public string SlackConnectionString { get; set; }

        public string SlackCommandToken { get; set; }

        public string SlackWebHookUri { get; set; }

        public string DBConnectionString { get; set; }
    }
}
