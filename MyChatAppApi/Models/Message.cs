using System;

namespace MyChatAppApi.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid? RoomId { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsSeen { get; set; }
    }
}
