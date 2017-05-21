using Attendance;
using Microsoft.EntityFrameworkCore;
using SlackLibrary.Interfaces;
using SlackLibrary.Models;
using WebChat.DAL;

public class MessageDBContext : DbContext
{
    public DbSet<DbSlackMessage> Messages { get; set; }

    private ISlackConfiguration _slackConfiguration { get; set; }

    public MessageDBContext(ISlackConfiguration slackConfiguration)
    {
        _slackConfiguration = slackConfiguration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_slackConfiguration.DBConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbSlackMessage>().ToTable("Messages");
    }
}