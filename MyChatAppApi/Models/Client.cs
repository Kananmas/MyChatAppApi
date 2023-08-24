using System;
using System.Collections.Generic;

namespace MyChatAppApi.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? FamliyName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Type { get; set; }
        public string? PhoneNumber { get; set; }


        public ICollection<Room> Room { get; set; }
    }
}
