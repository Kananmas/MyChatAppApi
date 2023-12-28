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
    public class UserRepositoryService : IUserRepositoryService
    {
        protected readonly ChatHubContext _chatHubContext;
        protected DbSet<Client> _users;

        public UserRepositoryService(ChatHubContext chatHubContext)
        {
            _chatHubContext = chatHubContext;
            _users = chatHubContext.Clients ;
        }

        public async Task AddNewUser(Client user)
        {
            await _users.AddAsync(user);

            _chatHubContext.SaveChanges();
        }

        public async Task DeleteUserById(Guid id)
        {
            await _users.Where(user => user.Id == id).ExecuteDeleteAsync();

            await _chatHubContext.SaveChangesAsync();
        }

        public async Task<Client> GetUserByEmailAndPassword(string email, string password)
        {
            return await _users.Where(user => user.Email == email && user.Password == password).FirstOrDefaultAsync();  
        }

        public async Task DeleteUserByPhoneNumber(string phoneNumber)
        {   
            await _users.Where(user => user.PhoneNumber == phoneNumber).ExecuteDeleteAsync();

            await _chatHubContext.SaveChangesAsync();
        }

        public async Task<List<User_DTO>> GetAllUsers()
        {
            var userList = new List<User_DTO>();

            foreach (var user in _users)
            {
                userList.Add(new User_DTO() 
                { 
                    Id = user.Id, 
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName 
                });
            }

            return userList;
        }

        public async Task<User_DTO> GetUserByEmail(string email)
        {
            var user = await _users.Where(user => user.Email == email).FirstOrDefaultAsync();

            return new User_DTO()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

        }

        public async Task<User_DTO> GetUserById(Guid id)
        {
            var user = await _users.Where(user => user.Id == id).FirstOrDefaultAsync();

            return new User_DTO()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

        }

        public async Task<User_DTO> GetUserByName(string name)
        {
            var user = await _users.Where(user => user.UserName == name).FirstOrDefaultAsync();

            return new User_DTO()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id
            };
        }

        public async Task<User_DTO> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _users.Where(user => user.PhoneNumber == phoneNumber).FirstOrDefaultAsync();

            return new User_DTO() { UserName = user.UserName, Email = user.Email, PhoneNumber = user.PhoneNumber, Id = user.Id };
        }
        public async Task UpdatePhonenumber(Guid id, string phonenumber)
        {
            var user = await _users.Where(user => user.Id == id).FirstOrDefaultAsync();

            user.PhoneNumber = phonenumber;


            _users.Update(user);
            await _chatHubContext.SaveChangesAsync();
        }

        public async Task UpdateUserEmail(Guid id, string email)
        {
            var user = await _users.Where(user => user.Id == id).FirstOrDefaultAsync();

            user.Email = email;


            _users.Update(user);
            await _chatHubContext.SaveChangesAsync();

        }

        public async Task UpdateUsername(Guid id, string username)
        {
            var user = await _users.Where(user => user.Id == id).FirstOrDefaultAsync();

            user.UserName = username;


            _users.Update(user);


            await _chatHubContext.SaveChangesAsync();
        }
    }
}
