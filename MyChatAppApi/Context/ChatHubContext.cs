﻿using Microsoft.AspNet.SignalR.Messaging;
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
        public DbSet<Client> Clients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<GroupSubscribtion> Subscribtions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connstring = "Server=localhost,3306;Database=chathub;Uid=root;Pwd=IloveDocker@1234!;";
            optionsBuilder.UseMySql(connstring, ServerVersion.AutoDetect(connstring));
        }


    }
}
