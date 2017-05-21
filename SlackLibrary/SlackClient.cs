using Newtonsoft.Json;
using SlackLibrary.Interfaces;
using SlackLibrary.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SlackLibrary
{
    public class SlackClient : ISlackClient
    {
        public ISlackConfiguration SlackConfiguration { get; set; }

        public SlackClient(ISlackConfiguration SlackConfiguration)
        {
            this.SlackConfiguration = SlackConfiguration;
        }

        public async Task PostMessage(string text, string userName = null, string channel = null)
        {
            SlackMessage message = new SlackMessage()
            {
                Channel = channel,
                UserName = userName,
                Text = text
            };

            await PostMessage(message);
        }

        public async Task PostMessage(SlackMessage message)
        {
            string jsonMessage = JsonConvert.SerializeObject(message);

            using (HttpClient webClient = new HttpClient())
            {
                var data = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
                {
                   new KeyValuePair<string, string>("payload", jsonMessage)
                });

                var response = await webClient.PostAsync(this.SlackConfiguration.Uri.AbsoluteUri,data);
            }
        }

    }
}
