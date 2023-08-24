using Microsoft.EntityFrameworkCore;
using MyChatAppApi.Context;
using MyChatAppApi.DTOs;
using MyChatAppApi.Models;
using MyChatAppApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatAppApi.Repository.Services
{
    public class MessageRepositoryService: IMessageRepositoryService
    {
        protected readonly ChatHubContext _chatHubContext;
        protected readonly DbSet<Message> _messages;

        public MessageRepositoryService(ChatHubContext chatHubContext)
        {
            _chatHubContext = chatHubContext;
            _messages = chatHubContext.Messages;
        }

        public async Task AddMessage(Message message)
        {
            await _messages.AddAsync(message);  

            await _chatHubContext.SaveChangesAsync();
        }

        public async Task DeleteMessage(Guid id)
        {
           Message target =  await _messages.Where(Message => Message.Id == id).FirstOrDefaultAsync();
           
            if(target != null)
            {
                _messages.Remove(target);

                _chatHubContext.SaveChanges();
            }
        }

        public async Task DeleteMessages(Guid[] ids)
        {
            List<Message> targets = new List<Message>();

            foreach(var message in _messages)
            {
                if(ids.Contains(message.Id)) targets.Add(message);
            }

            if(targets.Count > 0)
            {
                _messages.RemoveRange(targets);

                await _chatHubContext.SaveChangesAsync();
            }
        }

        public async Task<Message_DTO> GetMessageById(Guid id)
        {
            Message_DTO message = new Message_DTO();
            var target = await _messages.Where(message => message.Id == id).FirstOrDefaultAsync();

            if(target != null)
            {
                message.Id = target.Id;
                message.SenderName = target.SenderName;
                message.CreatedDate = target.CreatedDate;
                message.Text = target.Text;
                message.IsSeen = (bool) target.IsSeen;
            }


            return message;

        }

        public async Task<List<Message_DTO>> GetMessagesByRoomId(Guid roomId)
        {
            var messages = await _messages.Where(message => message.RoomId ==  roomId).ToListAsync();

            var messageDTOList = new List<Message_DTO>();


            foreach(var message in messages)
            {
                messageDTOList.Add(new Message_DTO()
                {
                    Id = message.Id,
                    CreatedDate = message.CreatedDate,
                    Text = message.Text,
                    SenderName = message.SenderName,
                    RoomId = roomId,
                    IsSeen = (bool) message.IsSeen  
                });

            }


            return messageDTOList;
        }

        public async Task UpdateMessage(Guid messageId, string messageText)
        {
            var Message = await _messages.Where(Message => Message.Id == messageId).FirstOrDefaultAsync();

            Message.Text = messageText;

            _messages.Update(Message);

            await _chatHubContext.SaveChangesAsync();
        }

        public async Task UpdateMessageStatus(Guid messageId, bool status)
        {
            var Message = await _messages.Where(Message => Message.Id == messageId).FirstOrDefaultAsync();

            Message.IsSeen = status;

            _messages.Update(Message);

            await _chatHubContext.SaveChangesAsync();
        }
    }
}
