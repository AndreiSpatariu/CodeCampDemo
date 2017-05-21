using SlackLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebChat.DAL;

public interface ISlackMessageStore
{
    List<DbSlackMessage> GetMessages();
    void AddMessage(DbSlackMessage message);
}