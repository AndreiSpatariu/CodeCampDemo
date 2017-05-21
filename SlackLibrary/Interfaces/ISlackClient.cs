using SlackLibrary.Models;
using System.Threading.Tasks;

namespace SlackLibrary.Interfaces
{
    public interface ISlackClient
    {
        ISlackConfiguration SlackConfiguration { get; set; }

        Task PostMessage(string text, string userName = null, string channel = null);

        Task PostMessage(SlackMessage message);
    }
}
