using System;

namespace MyChatAppApi.Models
{
    public class GroupSubscribtion
    {
        public Guid Id { get; set; }
        public Guid SubscriberId { get; set; }
        public Guid GroupId { get; set; }
        public DateTime JoinedTime { get; set; }

    }
}
