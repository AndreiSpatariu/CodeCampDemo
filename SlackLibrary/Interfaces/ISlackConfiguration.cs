using System;
using System.Collections.Generic;
using System.Text;

namespace SlackLibrary.Interfaces
{
    public interface ISlackConfiguration
    {
        string SlackCommandToken { get; set; }

        string SlackConnectionString { get; set; }

        Uri Uri { get; set; }

        string DBConnectionString { get; set; }
    }
}
