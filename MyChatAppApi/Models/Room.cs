using System;
using System.Collections.Generic;

namespace MyChatAppApi.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public Guid GroupOwnerId { get; set; }
        public string Name { get; set; }
        public string? LastMessage { get; set; }
        public DateTime StartingDate { get; set; }
    }
}
