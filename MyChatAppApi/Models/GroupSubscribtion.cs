using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyChatAppApi.Models
{
    public class GroupSubscribtion
    {
        public Guid Id { get; set; }
        [ForeignKey("Users")]
        public Guid SubscriberId { get; set; }
        [ForeignKey("Rooms")]
        public Guid GroupId { get; set; }
        public DateTime JoinedTime { get; set; }

    }
}
