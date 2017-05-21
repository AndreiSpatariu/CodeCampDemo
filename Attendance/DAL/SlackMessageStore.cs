using SlackLibrary.Interfaces;
using SlackLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.DAL;

public class SlackMessageStore : ISlackMessageStore
{
    // private MessageDBContext _dbContext { get; set; }
    private IServiceProvider serviceProvider { get; set; }

    public SlackMessageStore(IServiceProvider serviceProvider)
    {
       this.serviceProvider = serviceProvider;
    }

    public void AddMessage(DbSlackMessage message)
    {
        var _dbContext = (MessageDBContext)serviceProvider.GetService(typeof(MessageDBContext));
        
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
        
        
    }

    public List<DbSlackMessage> GetMessages()
    {
        List<DbSlackMessage> test = ((MessageDBContext)serviceProvider
            .GetService(typeof(MessageDBContext)))
            .Messages
            .OrderByDescending(m => m.Id)
            .ToList();

        return test;
    }
}