using MyChatAppApi.DTOs;
using MyChatAppApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyChatAppApi.Repository.Interfaces
{
    public interface IUserRepositoryService
    {
        Task<List<User_DTO>> GetAllUsers();
        Task<User_DTO> GetUserById(Guid id);
        Task<User_DTO> GetUserByName(string name);
        Task<User_DTO> GetUserByEmail(string email);
        Task<User_DTO> GetUserByPhoneNumber(string phoneNumber);
        Task<Client> GetUserByEmailAndPassword(string email, string password);
        Task DeleteUserByPhoneNumber(string phoneNumber);
        Task DeleteUserById(Guid id);

        Task UpdateUsername(Guid id,string username);
        Task UpdatePhonenumber(Guid id,string phonenumber);
        Task UpdateUserEmail(Guid id, string email);


        Task AddNewUser(Client user);
    }
}
