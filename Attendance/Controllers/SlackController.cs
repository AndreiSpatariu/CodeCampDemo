using Attendance.Handlers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackLibrary.Interfaces;
using SlackLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebChat.DAL;

namespace Attendance.Controllers
{
    public class SlackController : Controller
    {
        public ISlackClient SlackClient { get; set; }

        private ISlackMessageStore _slackMessageStore { get; set; }

        private SlackHandler _slackHandler { get; set; }

        public SlackController(ISlackClient slackClient, SlackHandler slackHandler, ISlackMessageStore slackMessageStore)
        {
            SlackClient = slackClient;
            _slackHandler = slackHandler;
            _slackMessageStore = slackMessageStore;
        }

        private async Task SendMessage([FromQueryAttribute]SlackMessage message)
        {
            await _slackHandler.InvokeClientMethodToAllAsync("receiveMessage", message.Text, message.UserName);
        }

        [HttpPost]
        public async void PostMessageToSlack(SlackMessage message)
        {
            var dbMessage = new DbSlackMessage()
            {
                Text = message.Text,
                UserName = message.UserName
            };

            await SlackClient.PostMessage(message);
            await SendMessage(message);
            _slackMessageStore.AddMessage(dbMessage);
        }

        [HttpPost]
        public async void PostMessageToWeb([FromBody]JToken commandRequest)
        {
            var command = JsonConvert.DeserializeObject<SlackCommand>(commandRequest.ToString());

            if (command.Token == SlackClient.SlackConfiguration.SlackCommandToken)
            {
                await SlackClient.PostMessage(text: command.Text, userName: command.UserName);

                await SendMessage(new SlackMessage()
                {
                    UserName = command.UserName,
                    Text = command.Text
                });

                _slackMessageStore.AddMessage(new DbSlackMessage()
                {
                    UserName = command.UserName,
                    Text = command.Text
                });
            }
        }
        
        [HttpGet]
        public List<DbSlackMessage> GetMessages()
        {
            return _slackMessageStore.GetMessages();
        }
    }
}
