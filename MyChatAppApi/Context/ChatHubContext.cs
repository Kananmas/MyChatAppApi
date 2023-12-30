using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyChatAppApi.Models;
using System;
using Message = MyChatAppApi.Models.Message;

namespace MyChatAppApi.Context
{
    public class ChatHubContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChatHubContext> _logger;   


        public ChatHubContext(IConfiguration configuration , ILogger<ChatHubContext> logger ,
            DbContextOptions<ChatHubContext> options):base(options)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<GroupSubscribtion> Subscribtions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connstring = _configuration["ConnectionStrings:DefaultConnection"];

            try
            {

                optionsBuilder.UseMySql(connstring, ServerVersion.AutoDetect(connstring));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(connstring);
            }
        }


    }
}
