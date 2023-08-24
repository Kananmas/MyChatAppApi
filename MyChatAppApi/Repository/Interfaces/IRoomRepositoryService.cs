using Microsoft.AspNet.SignalR.Messaging;
using MyChatAppApi.DTOs;
using MyChatAppApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyChatAppApi.Repository.Interfaces
{
    public interface IRoomRepositoryService
    {
        Task<List<Room>> GetRoomList(List<Guid> roomIds);
        Task<List<Room>> GetRoomsByName(string roomName);
        Task<Room> GetRoom(Guid roomId);

        Task CreateRoom(Room room);

        Task DeleteRoom(Guid roomId);
        Task DeleteRooms(List<Guid> roomIds);

        Task UpdateRoomName(Guid roomId, string roomName);
    }
}
