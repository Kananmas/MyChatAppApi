using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyChatAppApi.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        [ForeignKey("Room")]
        public Guid? RoomId { get; set; }
        public string? SenderName { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsSeen { get; set; }
    }
}
