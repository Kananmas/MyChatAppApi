using MyChatAppApi.DTOs;
using MyChatAppApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyChatAppApi.Repository.Interfaces
{
    public interface IMessageRepositoryService
    {
        Task<Message_DTO> GetMessageById(Guid id);
        Task<List<Message_DTO>> GetMessagesByRoomId(Guid roomId);


        Task DeleteMessages(Guid[] ids);
        Task DeleteMessage(Guid id);
        

        Task AddMessage(Message message);

        Task UpdateMessage(Guid messageId, string messageText);
        Task UpdateMessageStatus(Guid messageId, bool status);
    }
}
