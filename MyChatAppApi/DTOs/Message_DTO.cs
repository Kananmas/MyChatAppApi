using MyChatAppApi.Models;
using System;
using System.Collections.Generic;

namespace MyChatAppApi.DTOs
{
    public class Message_DTO
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public string Text { get; set; }
        public string? SenderName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsSeen { get; set; }
    }
}
