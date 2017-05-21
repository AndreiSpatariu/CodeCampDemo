using SlackLibrary.Interfaces;
using System;

namespace Attendance
{
    public class SlackConfiguration : ISlackConfiguration
    {
        public Uri Uri { get; set; }

        public string SlackCommandToken { get; set; }

        public string SlackConnectionString { get; set; }

        public string DBConnectionString { get; set; }
    }
}