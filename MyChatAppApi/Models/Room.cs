using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyChatAppApi.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        [ForeignKey("Users")]
        public Guid GroupOwnerId { get; set; }
        public string Name { get; set; }
        public string? LastMessage { get; set; }
        public DateTime StartingDate { get; set; }


        public ICollection<Message> Messages { get; set; }
    }
}
