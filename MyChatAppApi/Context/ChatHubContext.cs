using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyChatAppApi.Models;
using Message = MyChatAppApi.Models.Message;

namespace MyChatAppApi.Context
{
    public class ChatHubContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public ChatHubContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connstring = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connstring, ServerVersion.AutoDetect(connstring));
        }


    }
}
