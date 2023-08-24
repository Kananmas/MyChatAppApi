using Microsoft.EntityFrameworkCore;
using MyChatAppApi.Context;
using MyChatAppApi.Models;
using MyChatAppApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatAppApi.Repository.Services
{
    public class RoomRepositoryService : IRoomRepositoryService
    {
        protected readonly ChatHubContext _chatHubContext;
        protected readonly DbSet<Room> _rooms;

        public RoomRepositoryService(ChatHubContext chatHubContext)
        {
            _chatHubContext = chatHubContext;
            _rooms = chatHubContext.Rooms;
        }

        public async Task DeleteRoom(Guid roomId)
        {
            await _rooms.Where(room => room.Id == roomId).ExecuteDeleteAsync();

            await _chatHubContext.SaveChangesAsync();
        }

        public async Task DeleteRooms(List<Guid> roomIds)
        {
            await _rooms.Where(room => roomIds.Contains(room.Id)).ExecuteDeleteAsync();

            await _chatHubContext.SaveChangesAsync();

        }

        public async Task<Room> GetRoom(Guid roomId)
        {
            var room = await _rooms.Where(room => room.Id == roomId).FirstOrDefaultAsync();

            return room;
        }

        public async Task CreateRoom(Room room)
        {
            await _rooms.AddAsync(room);

            await _chatHubContext.SaveChangesAsync();
        }

        public async Task<List<Room>> GetRoomList(List<Guid> roomIds)
        {
            var roomList = new List<Room>();

            foreach (var roomId in roomIds)
            {
                var targetRoom = await GetRoom(roomId);
                if (targetRoom != null) roomList.Add(targetRoom);
            }

            return roomList;
        }

        public async Task UpdateRoomName(Guid roomId, string roomName)
        {
           var room =  await _rooms.Where(room => room.Id == roomId).FirstOrDefaultAsync();

            room.Name = roomName;

            _rooms.Update(room);

            await _chatHubContext.SaveChangesAsync();
        }

        public async Task<List<Room>> GetRoomsByName(string roomName)
        {
            return await _rooms.Where(room => room.Name.Contains(roomName)).ToListAsync(); 
        }
    }
}
