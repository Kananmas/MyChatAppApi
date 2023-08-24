using ChatService.ConnectionMapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using MyChatAppApi.DTOs;
using MyChatAppApi.Models;
using MyChatAppApi.Repository.Interfaces;
using MyChatAppApi.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatAppApi.MainHub
{

    public class ChatHub : Hub
    {

        private readonly static BaseConnectionMapping<string> _connections = new BaseConnectionMapping<string>();
        private readonly IGroupSubscribtionRepositoryService _groupSubscribtionRepositoryService;
        private readonly IRoomRepositoryService _roomRepositoryService;
        private readonly IMessageRepositoryService _messageRepositoryService;
        private readonly IUserRepositoryService _userRepositoryService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ChatHub(IGroupSubscribtionRepositoryService groupSubscribtionRepositoryService, 
            IHttpContextAccessor contextAccessor,
            IRoomRepositoryService roomRepositoryService,
            IMessageRepositoryService messageRepositoryService,
            IUserRepositoryService userRepositoryService) : base()
        {
            _groupSubscribtionRepositoryService = groupSubscribtionRepositoryService;
            _contextAccessor = contextAccessor;
            _roomRepositoryService = roomRepositoryService;
            _messageRepositoryService = messageRepositoryService;
            _userRepositoryService = userRepositoryService;
        }

        public async Task GetAllRooms(string userId)
        {
            try
            {
                Guid _userId = Guid.Parse(userId);
                var groupsSubs = await _groupSubscribtionRepositoryService.GetAllUserSubs(_userId);
                var groupsIdList = new List<Guid>();

                foreach (var groupSub in groupsSubs) 
                { 
                    groupsIdList.Add(groupSub.GroupId);
                }

                var groups = await _roomRepositoryService.GetRoomList(groupsIdList);

                await Clients.Caller.SendAsync("RecivieAllRooms", groups);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task CreateNewGroup(string groupName, string creatorId)
        {
            var groupGuid = Guid.NewGuid();
            var newGroup = new Room()
            {
                Id = groupGuid,
                Name = groupName,
                StartingDate = DateTime.Now,
                GroupOwnerId = Guid.Parse(creatorId)
            };

            var groupSub = new GroupSubscribtion()
            {
                Id = Guid.NewGuid(),
                SubscriberId = Guid.Parse(creatorId),
                GroupId = groupGuid
                ,
            };


            await _groupSubscribtionRepositoryService.AddNewSubscribtion(groupSub);
            await _roomRepositoryService.CreateRoom(newGroup);
            await GetAllRooms(creatorId);
        }


        public async Task GetChatMessages(string chatRoomID)
        {
            var ID = Guid.Parse(chatRoomID);

            var messages = await _messageRepositoryService.GetMessagesByRoomId(ID);
            var subsribtions = await _groupSubscribtionRepositoryService.GetAllSubsByGroupId(ID);
            var userList = new List<string>();

            foreach (var sub in subsribtions)
            {
                var targetUser = await _userRepositoryService.GetUserById(sub.SubscriberId);

                if (targetUser != null) userList.Add(targetUser.UserName);
            }

     
            foreach (var user in userList)
            {
                string userConnection = _connections.GetConnections(user);
                if(userConnection != "" && userConnection != null) await Clients.Client(userConnection).SendAsync("RecivieChatMessages",messages.OrderBy(message => message.CreatedDate).ToList() );
            }
        }

        public async Task DeleteMessage(string id, string roomId)
        {
            var ID = Guid.Parse(id);
            await _messageRepositoryService.DeleteMessage(ID);
            await GetChatMessages(roomId);
        }

        public async Task GetChatMessagesForMe(string chatRoomID)
        {
            var ID = Guid.Parse(chatRoomID);

            var messages = await _messageRepositoryService.GetMessagesByRoomId(ID);


            await Clients.Caller.SendAsync("RecivieChatMessages", messages.OrderBy(message => message.CreatedDate).ToList());
        }

        public async Task SendNewMessage(string Text , string SelectedRoomID)
        {
            var commonUtillites = new CommonUtillites(_contextAccessor);
            var selectedRoomGuid = Guid.Parse(SelectedRoomID);
            var userName = commonUtillites.GetUserName();

            var message = new Message()
            {
                Text = Text,
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsSeen = false,
                SenderName = userName,
                RoomId = selectedRoomGuid,
            };

            await _messageRepositoryService.AddMessage(message);
            await GetChatMessages(SelectedRoomID);
        }

        public async Task SubscribeToGroup(string groupId)
        {
            var commonUtilites = new CommonUtillites(_contextAccessor);
            var userId = commonUtilites.GetUserID();

            var userSubs = await _groupSubscribtionRepositoryService.GetAllUserSubs(userId);
            var alreadySubbed = userSubs
                .Where(subs => subs.GroupId == Guid.Parse(groupId))
                .FirstOrDefault() != null;

            if(alreadySubbed)
            {
                await GetChatMessagesForMe(groupId);
                return;
            }

            var newSub = new GroupSubscribtion()
            {
                Id = Guid.NewGuid() ,
                GroupId = Guid.Parse(groupId),
                SubscriberId = userId,
                JoinedTime = DateTime.Now,
            };

            await _groupSubscribtionRepositoryService.AddNewSubscribtion(newSub);
            await GetChatMessagesForMe(groupId);
        }

        public async Task RemoveRoom(string roomID)
        {
            var commonUtitlites = new CommonUtillites(_contextAccessor);
            var userID = commonUtitlites.GetUserID();
            await _groupSubscribtionRepositoryService.RemoveSubscribtionForUser(Guid.Parse(roomID), userID);
            await GetAllRooms(userID.ToString());
        }

        public override Task OnConnectedAsync()
        {
            
            var commonUtilites = new CommonUtillites(_contextAccessor);
            var name = commonUtilites.GetUserName();
            var connectionId = Context.ConnectionId;

            _connections.Add(name, connectionId);


            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var commonUtilites = new CommonUtillites(_contextAccessor);
            var name = commonUtilites.GetUserName();
           
            _connections.Remove(name);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
